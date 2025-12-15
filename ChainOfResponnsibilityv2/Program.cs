public class CustomerRequest
{
    public string RequestType { get; set; }
    public string Description { get; set; }

    public CustomerRequest(string requestType, string description)
    {
        RequestType = requestType;
        Description = description;
    }
}





public abstract class  RequestHandler
{
    protected RequestHandler NextHandler;

    public void SetNextHandler(RequestHandler nextHandler)
    {
        NextHandler = nextHandler;
    }

    public abstract void HandleRequest(CustomerRequest request);    
}


public class CustomerSupportHandler : RequestHandler
{
    public override void HandleRequest(CustomerRequest request)
    {
        if (request.RequestType == "Customer Support")
        {
            Console.WriteLine($"Customer Support: Handling request - {request.Description}");
        }
        else if (NextHandler != null)
        {
            NextHandler.HandleRequest(request);
        }
    }
}
public class TechnicalSupportHandler : RequestHandler
{
    public override void HandleRequest(CustomerRequest request)
    {
        if (request.RequestType == "Technical Support")
        {
            Console.WriteLine($"Technical Support: Handling request - {request.Description}");
        }
        else if (NextHandler != null)
        {
            NextHandler.HandleRequest(request);
        }
    }
}
public class ManagementHandler : RequestHandler
{
    public override void HandleRequest(CustomerRequest request)
    {
        if (request.RequestType == "Management")
        {
            Console.WriteLine($"Management: Handling request - {request.Description}");
        }
        else if (NextHandler != null)
        {
            NextHandler.HandleRequest(request);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create handlers
        var customerSupport = new CustomerSupportHandler();
        var technicalSupport = new TechnicalSupportHandler();
        var management = new ManagementHandler();

        // Set up the chain
        customerSupport.SetNextHandler(technicalSupport);
        technicalSupport.SetNextHandler(management);

        // Create requests
        var request1 = new CustomerRequest("Customer Support", "Need help with my account.");
        var request2 = new CustomerRequest("Technical Support", "Having trouble with the website.");
        var request3 = new CustomerRequest("Management", "Feedback about service quality.");

        // Pass requests through the chain
        customerSupport.HandleRequest(request1);
        customerSupport.HandleRequest(request2);
        customerSupport.HandleRequest(request3);
    }
}

// Source: https://dev.to/dotnetfullstackdev/implementing-chain-of-responsibility-pattern-in-c-middlewares-design-pattern-16bl