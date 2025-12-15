
using System;
using System.Collections.Generic;

public enum RequestType
{
    OnDuty,
    Leave,
    AttendanceCorrection,
    Other
}

public enum ApprovalStatus
{
    Approved,
    Declined,
    InProgress,
    Pending
}

public enum ApprovalLevel
{
    Manager,
    DepartmentHead,
    Chief
}

public class ApprovalRequest
{
    public RequestType RequestType { get; set; }
    public ApprovalStatus Status { get; set; } = ApprovalStatus.Pending;
    public string Remarks { get; set; } = "";
}

public interface IApprover
{
    IApprover SetNext(IApprover handler);
    ApprovalRequest Handle(ApprovalRequest request);
}

abstract class ApproverBase : IApprover
{
    private IApprover _next;

    public IApprover SetNext(IApprover handler)
    {
        _next = handler;
        return handler;
    }

    public virtual ApprovalRequest Handle(ApprovalRequest request)
    {
        return _next?.Handle(request);
    }
}

class ManagerApprover : ApproverBase
{
    public override ApprovalRequest Handle(ApprovalRequest request)
    {
        if (request.RequestType == RequestType.OnDuty ||
            request.RequestType == RequestType.Leave ||
            request.RequestType == RequestType.AttendanceCorrection)
        {
            Console.WriteLine("Manager: Approved.");
            request.Remarks += "Manager approved. ";
            request.Status = ApprovalStatus.InProgress;
        }

        return base.Handle(request);
    }
}

class DepartmentHeadApprover : ApproverBase
{
    public override ApprovalRequest Handle(ApprovalRequest request)
    {
        if (request.RequestType == RequestType.Leave ||
            request.RequestType == RequestType.AttendanceCorrection)
        {
            Console.WriteLine("Department Head: Approved.");
            request.Remarks += "Department Head approved. ";
            request.Status = ApprovalStatus.InProgress;
        }

        return base.Handle(request);
    }
}

class ChiefApprover : ApproverBase
{
    public override ApprovalRequest Handle(ApprovalRequest request)
    {
        Console.WriteLine("Chief: Approved.");
        request.Remarks += "Chief approved. ";
        request.Status = ApprovalStatus.Approved;

        return base.Handle(request);
    }
}

class Client
{
    public static void Run(ApprovalRequest request, IApprover chain)
    {
        Console.WriteLine($"\nRequest: {request.RequestType}");
        var result = chain.Handle(request);

        Console.WriteLine($"Final Status: {result.Status}");
        Console.WriteLine($"Remarks: {result.Remarks}\n");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== APPROVAL CHAIN SIMULATION ===\n");

        // Scenario 1: Manager only, On-Duty request
        Console.WriteLine("Scenario 1: Manager Only (On-Duty)");
        var s1 = new ManagerApprover();
        Client.Run(new ApprovalRequest { RequestType = RequestType.OnDuty }, s1);

        // Scenario 2: Manager -> Department Head (Leave / Attendance Correction)
        Console.WriteLine("Scenario 2: Manager -> Department Head (Leave & Attendance Correction)");
        var s2 = new ManagerApprover();
        s2.SetNext(new DepartmentHeadApprover());
        Client.Run(new ApprovalRequest { RequestType = RequestType.Leave }, s2);
        Client.Run(new ApprovalRequest { RequestType = RequestType.AttendanceCorrection }, s2);

        // Scenario 3: Manager’s request directly to Department Head
        Console.WriteLine("Scenario 3: Goes Directly to Department Head");
        var s3 = new DepartmentHeadApprover();
        Client.Run(new ApprovalRequest { RequestType = RequestType.Leave }, s3);

        // Special Scenario: Manager -> Department Head -> Chief
        Console.WriteLine("Special Scenario: Manager -> Department Head -> Chief");
        var s4 = new ManagerApprover();
        s4.SetNext(new DepartmentHeadApprover())
          .SetNext(new ChiefApprover());
        Client.Run(new ApprovalRequest { RequestType = RequestType.Other }, s4);
    }
}


// TODO: Scenario 1: Manager is the only approver of On-Duty Request
// TODO: Scenario 2: Manager is the First approval and the Department Head is the Final approver of Leave Request and Attensdance Correction Request, Chief is not Involved
// TODO: Scenario 3: Manager's  request Directly to the Department Head
// TODO: Special Scenario: Manager , Department Head then the chief is the sequence of all the Approval


//  Flow
// Employee apply for Leave Request
// Then the Manager approve it
// then it goes to Department Head for approval
// following those Scenario