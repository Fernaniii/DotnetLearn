

// Why to Use Generic?
// one of the Uses is to Send Parameter is to a Dapper

using Generic_Demo;



/*

 
 */
 
 
MathOperations<int> inMath = new();
Console.WriteLine(inMath.Add(1, 4));
Console.WriteLine(inMath.Multiply(1, 4));
Console.WriteLine(inMath.Subtract(1, 4));
Console.WriteLine(inMath.Divide(1, 4));

MathOperations<double> doublemath = new();
Console.WriteLine(doublemath.Add(1, 7.5));
Console.WriteLine(doublemath.Subtract(1, 7.5));
Console.WriteLine(doublemath.Multiply(1, 7.5));
Console.WriteLine(doublemath.Divide(1, 7.5));
 


/*
 
 // Using the Generic class
BetterList<int> betternumbers = new();
betternumbers.AddToList(6);

BetterList<PersonRecord> people = new();
people.AddToList( new("Fernan", "Magnaye" ));

record PersonRecord(string FirstName, string LastName);
 
 */





/*
 
List<int> numbers = [1, 2, 3];
List<string> strings = ["tim", "fernan"];

TypeChecker(1);
TypeChecker("Fernan");
TypeChecker(new PersonRecord("Fernando", "Magnaye"));

// this Method using Generic
void TypeChecker<T>(T value)
{
    Console.WriteLine($"Type: {typeof(T)}");
    Console.WriteLine($"Value: {value}");
}

record PersonRecord(string FirstName, string LastName);

 
 */



/*
 
 //* List<object> instead of Generic T
 
// This Show the Comparisson of Speed of Using objects instead of T in a List of number
// this Test Compared the Ellapsed time Between int and object using Stop Watch
List<int> numbers = [1, 2, 3];

//This is Invalid
//List<string> strings = ["tim", "fernan", 4];
List<string> strings = ["tim", "fernan"];

// this Work but this is not Efficient for Some Reason
// Don't Use this
List<object> objects = ["fernan", 4];

Stopwatch sw = new();
sw.Start();
for (int i = 0; i < 1_000_000; i++)
{
    objects.Add(i);
}
sw.Stop();
Console.WriteLine($"List<objects> elapsed time: {sw.ElapsedMilliseconds}");

sw = new();
sw.Start();
for (int i = 0; i < 1_000_000; i++)
{
    numbers.Add(i);
}
sw.Stop();
Console.WriteLine($"List<int> elapsed time: {sw.ElapsedMilliseconds}");


 
 */
