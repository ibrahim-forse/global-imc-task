using GlobalIMCTask.Domain.Products;
using GlobalIMCTask.Tests.Mock.Products;
using System;

namespace GlobalIMCTask.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            string guid = Guid.NewGuid().ToString();
            Console.WriteLine(guid);
        }
        public static bool CheckURLValid(string source)
        {
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }


}
