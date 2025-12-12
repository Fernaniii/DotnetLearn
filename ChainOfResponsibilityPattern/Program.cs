

    //public enum RequestType
    //{
    //    OnDuty,
    //    Leave,
    //    AttendanceCorrection,
    //    Other
    //}


    public interface IApprover
    {
        IApprover SetNext(IApprover handler);

        object Approve(object request);
    }

    // The default chaining behavior can be implemented inside a base handler
    // class.
    abstract class AbstractApprover : IApprover
    {
        private IApprover _nextHandler;

        public IApprover SetNext(IApprover handler)
        {
            this._nextHandler = handler;

            // Returning a handler from here will let us link handlers in a
            // convenient way like this:
            // monkey.SetNext(squirrel).SetNext(dog);
            return handler;
        }

        public virtual object Approve(object request)
        {
            if (this._nextHandler != null)
            {
                return this._nextHandler.Approve(request);
            }
            else
            {
                return null;
            }
        }
    }

    class ManagerApprover : AbstractApprover
    {
        public override object Approve(object request)
        {
            if ((request as string) == "On-Duty")
            {
                return $"Manager: I'll Approve the {request.ToString()}.\n";
            }
            else
            {
                return base.Approve(request);
            }
        }
    }

    class DeptHeadApprover : AbstractApprover
    {
        public override object Approve(object request)
        {
            if (request.ToString() == "Leave")
            {
                return $"DeptHead: I'll Approve the {request.ToString()}.\n";
            }
            else
            {
                return base.Approve(request);
            }
        }
    }


    class ChiefApprover : AbstractApprover
    {
        public override object Approve(object request)
        {
            if (request.ToString() == "Attendance Correction")
            {
                return $"Chief: I'll Approve the {request.ToString()}.\n";
            }
            else
            {
                return base.Approve(request);
            }
        }
    }

    class Employee
    {
        // The client code is usually suited to work with a single handler. In
        // most cases, it is not even aware that the handler is part of a chain.
        public static void EmployeeCode(AbstractApprover handler)
        {
            foreach (var request in new List<string> { "On-Duty", "Leave", "Attendance Correction" })
            {
                Console.WriteLine($"Employee: Who can Approve my {request}?");

                var result = handler.Approve(request);

                if (result != null)
                {
                    Console.Write($"   {result}");
                }
                else
                {
                    Console.WriteLine($"   {request} was left untouched.");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The other part of the client code constructs the actual chain.
            var manager = new ManagerApprover();
            var departmentHead = new DeptHeadApprover();
            var chiefApprover = new ChiefApprover();

            var Sequence = manager.SetNext(departmentHead).SetNext(chiefApprover);

            // The client should be able to send a request to any handler, not
            // just the first one in the chain.
            Console.WriteLine("Chain: Manager > Department Head > Chief Approver\n");
            Employee.EmployeeCode(manager);
            Console.WriteLine();


            Console.WriteLine("Subchain: Department Head > Chief Approver\n");
            Employee.EmployeeCode(departmentHead);
            Console.WriteLine();


            Console.WriteLine("Sub Chain: Chief Approver\n");
            Employee.EmployeeCode(chiefApprover);
            Console.WriteLine();
    }
    }

//  Scenario 1: Manager is the only approver of On-Duty Request
//  Scenario 2: Manager is the First approval and the Department Head is the Final approver of Leave Request and Attensdance Correction Request, Chief is not Involved
//  Scenario 3: Manager's  request Directly to the Department Head
//  Special Scenario: Manager , Department Head then the chief is the sequence of all the Approval
