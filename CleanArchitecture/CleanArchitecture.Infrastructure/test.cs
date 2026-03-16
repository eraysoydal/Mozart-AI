using System;
using System.Reflection;
class Program { 
    static void Main() {
        var asm = Assembly.Load("AWSSDK.CloudFront");
        foreach(var t in asm.GetExportedTypes()) {
            if (t.Name.Contains("Sign")) Console.WriteLine(t.FullName);
        }
    }
}
