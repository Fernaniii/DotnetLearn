### Objectives
- Why Generic i Important 
- How to use Generic 
- What is Generic Type
- How to limit Generic
`How to Create C# Generic` by Tim Corey

- Using Generic you can use different type or type or List just by Using `T`
- It is Type Safe Meaning that you cannot add different type of argument/type once you Declare What type to Use

```C#
List<int> numbers = [1, 2, 3];
List<string> strings = ["tim", "fernan"];
```
How can you use this Both different  type if you want to be Handle by one class or Method?

you can use `object` for this Matter but it will be so slow

```C#
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
```

![[Pasted image 20251109214532.png]]

So It is Better to Use `T` type as the Generics
``` C# Generics
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
```

With class
```C#
public class MathOperations<T> where T : INumber<T>
{

	public T Add(T x, T y)
	{
		return x + y;
	}

	public T Subtract(T x, T y) { return x - y; }
	public T Multiply(T x, T y) { return x * y; }

	public T Divide(T x, T y) {return x / y; }

}
```
`where` is the Condition or something like type Validation to Check What type it Should be

```C#
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
```
Condition is to Make sure the Provided Parameter is number

More on `/LearnDotNet/GenericDemo` Project
