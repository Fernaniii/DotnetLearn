using System.Security.Authentication.ExtendedProtection;

public enum ApprovalStatus
{
    Pending,
    InProgress,
    Approved,
    Rejected
}

public sealed class LeaveRequest
{
    public int EmployeeId { get; }
    public int NumberOfDays { get; }
    public string Reason { get; }

    public ApprovalStatus Status { get; private set; } = ApprovalStatus.Pending;
    public string? LastApprovedBy { get; private set; }

    public LeaveRequest(int employeeId, int numberOfDays, string reason)
    {
        if (numberOfDays <= 0)
            throw new ArgumentOutOfRangeException(nameof(numberOfDays));

        EmployeeId = employeeId;
        NumberOfDays = numberOfDays;
        Reason = reason ?? throw new ArgumentNullException(nameof(reason));
    }

    public void MarkInProgress(string approver)
    {
        Status = ApprovalStatus.InProgress;
        LastApprovedBy = approver;
    }

    public void MarkApproved(string approver)
    {
        Status = ApprovalStatus.Approved;
        LastApprovedBy = approver;
    }

    public void MarkRejected(string approver)
    {
        Status = ApprovalStatus.Rejected;
        LastApprovedBy = approver;
    }
}

public interface ILeaveApprover
{
    ILeaveApprover SetNext(ILeaveApprover next);
    void Approve(LeaveRequest request);
}

public abstract class LeaveApproverBase : ILeaveApprover
{
    private ILeaveApprover? _next;

    public ILeaveApprover SetNext(ILeaveApprover next)
    {
        _next = next;
        return next;
    }

    public void Approve(LeaveRequest request)
    {
        if (_next is null)
        {
            request.MarkApproved(GetApproverName());
        }
        else
        {
            request.MarkInProgress(GetApproverName());
            //_next.Approve(request);
        }
    }

    protected abstract string GetApproverName();
}



public sealed class ManagerApprover : LeaveApproverBase
{
    protected override string GetApproverName()
        => "Manager";
}

public sealed class DepartmentHeadApprover : LeaveApproverBase
{
    protected override string GetApproverName()
        => "Department Head";
}

public sealed class ChiefApprover : LeaveApproverBase
{
    protected override string GetApproverName()
        => "Chief / HR";
}


class Program
{
    static void Main()
    {
        // Build approval chain
        var manager = new ManagerApprover();
        var deptHead = new DepartmentHeadApprover();
        var chief = new ChiefApprover();

        //chief.SetNext(null);
       //manager.SetNext(deptHead).SetNext(chief);

        // Create leave request
        var leaveRequest = new LeaveRequest(
            employeeId: 1,
            numberOfDays: 5,
            reason: "Medical Leave"
        );

        // Scenario 1
        Console.WriteLine("manager only");
        manager.SetNext(null);
        manager.Approve(leaveRequest);
        Console.WriteLine($"Status           : {leaveRequest.Status}");
        Console.WriteLine($"Last Approved By : {leaveRequest.LastApprovedBy}");
        Console.WriteLine("\n");


        // Scenario 2
        Console.WriteLine("manager > dept head");
        manager.SetNext(deptHead);

        manager.Approve(leaveRequest);
        Console.WriteLine($"Status           : {leaveRequest.Status}");
        Console.WriteLine($"Last Approved By : {leaveRequest.LastApprovedBy}");

        deptHead.Approve(leaveRequest);
        Console.WriteLine($"Status           : {leaveRequest.Status}");
        Console.WriteLine($"Last Approved By : {leaveRequest.LastApprovedBy}");


        Console.WriteLine("\n \n");
        // Scenario 3
        Console.WriteLine("manager > deptHead > chief");
        manager.SetNext(deptHead).SetNext(chief);

        manager.Approve(leaveRequest);
        Console.WriteLine($"Status           : {leaveRequest.Status}");
        Console.WriteLine($"Last Approved By : {leaveRequest.LastApprovedBy}");

        deptHead.Approve(leaveRequest);
        Console.WriteLine($"Status           : {leaveRequest.Status}");
        Console.WriteLine($"Last Approved By : {leaveRequest.LastApprovedBy}");

        chief.Approve(leaveRequest);
        Console.WriteLine($"Status           : {leaveRequest.Status}");
        Console.WriteLine($"Last Approved By : {leaveRequest.LastApprovedBy}");

        Console.WriteLine("\n \n");
        Console.WriteLine("Directly to Chief");
        chief.SetNext(null);
        chief.Approve(leaveRequest);
        Console.WriteLine($"Status           : {leaveRequest.Status}");
        Console.WriteLine($"Last Approved By : {leaveRequest.LastApprovedBy}");
        Console.WriteLine("\n");


    }
}


// TODO: Scenario 1: Manager is the only approver of On-Duty Request
// TODO: Scenario 2: Manager is the First approval and the Department Head is the Final approver of Leave Request and Attensdance Correction Request, Chief is not Involved
// TODO: Scenario 3: Manager , Department Head then the chief is the sequence of all the Approval
// TODO: Special Scenario 3: Manager's  request Directly to the Department Head
