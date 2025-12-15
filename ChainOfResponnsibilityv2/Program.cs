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
        //if (!CanApprove(request))
        //{
        //    if (_next is not null)
        //    {
        //        _next.Approve(request);
        //    }
        //    else
        //    {
        //        request.MarkRejected(GetApproverName());
        //    }

        //    return;
        //}

        if (_next is null)
        {
            // Final approver (Chief / HR)
            request.MarkApproved(GetApproverName());
        }
        else
        {
            // Intermediate approver
            request.MarkInProgress(GetApproverName());
            _next.Approve(request);
        }
    }

    //protected abstract bool CanApprove(LeaveRequest request);
    protected abstract string GetApproverName();
}



public sealed class ManagerApprover : LeaveApproverBase
{
    private const int MaxDays = 3;

    //protected override bool CanApprove(LeaveRequest request)
    //    => request.NumberOfDays <= MaxDays;

    protected override string GetApproverName()
        => "Manager";
}

public sealed class DepartmentHeadApprover : LeaveApproverBase
{
    private const int MaxDays = 7;

    //protected override bool CanApprove(LeaveRequest request)
    //    => request.NumberOfDays <= MaxDays;

    protected override string GetApproverName()
        => "Department Head";
}

public sealed class ChiefApprover : LeaveApproverBase
{
    //protected override bool CanApprove(LeaveRequest request)
    //    => true;

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

        manager.SetNext(deptHead).SetNext(chief);

        // Create leave request
        var leaveRequest = new LeaveRequest(
            employeeId: 1,
            numberOfDays: 5,
            reason: "Medical Leave"
        );

        // Process approval
        manager.Approve(leaveRequest);

        // Output result
        Console.WriteLine($"Status           : {leaveRequest.Status}");
        Console.WriteLine($"Last Approved By : {leaveRequest.LastApprovedBy}");
    }
}

