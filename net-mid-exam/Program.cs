using System.Reflection;

// 1. Load library
string dllPath = Path.Combine(Environment.CurrentDirectory, "ExternalLibrary.dll");
Assembly? assembly = Assembly.LoadFile(dllPath);

// 2. Get class type
Type? type = assembly.GetType("ExternalLibrary.SampleClass");

object[] objArray = new object[20];

// 3. Fill aray with instances
object? instance;
Random rand = new Random();
for (int i = 0; i < objArray.Length; i++)
{
    // Create SampleClass instance with its constructor parameters (id, name, size)
    instance = Activator.CreateInstance(type!, i + 1, $"Instance{i + 1}", rand.Next(1, 10));
    objArray[i] = instance!;
}

// 4. Sort the array by size
objArray = objArray.OrderBy(obj => GetPropertyValue(type!, "Size", obj)).ToArray();

// 5. Print out array
Console.WriteLine("Id     Name         Size");
for (int i = 0; i < objArray.Length; i++)
{
    PrintWithFormat(type!, objArray[i]);
}

// Helper functions
// Get object property value
object? GetPropertyValue(Type type, string propertyName, object obj)
{
    return type.GetProperty(propertyName)!.GetValue(obj, null);
}

// Just to print information beautifully in console matching Id Name Size header
void PrintWithFormat(Type type, object obj)
{
    string? id = GetPropertyValue(type, "Id", obj)!.ToString();
    string? name = GetPropertyValue(type, "Name", obj)!.ToString();
    string? size = GetPropertyValue(type, "Size", obj)!.ToString();
    Console.WriteLine($"{id}{new string(' ', 7 - id!.Length)}{name}{new string(' ', 13 - name!.Length)}{size}");
}