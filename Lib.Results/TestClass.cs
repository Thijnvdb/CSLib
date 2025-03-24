using static Lib.Results.ResultTypes;

namespace Lib.Results;

public class TestClass
{
    public void TestResult()
    {
        var result = GetResult();
        if (result.IsSuccess(out var data))
        {
            Console.WriteLine(data);
        }

        if (result.IsError(out var error))
        {
            throw error.Exception ?? new Exception();
        }

        string test = result switch
        {
            Result<string>.Ok(string value) => value,
            Result<string>.Err(var err) => err.Message,
            _ => string.Empty,
        };
    }

    public Result<string, Error> GetResult()
    {
        if (DateTime.Now.Second % 2 == 0)
        {
            return Ok("This is ok!");
        }

        return Error.Generic("this is an error!");
    }
}