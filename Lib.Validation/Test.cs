namespace Lib.Validation.Test;

record Person(string Name, int Age, int HeightInCm, IEnumerable<string> Nicknames);

class TestValidator : Validator<Person>
{
    protected override IEnumerable<IValidationRule<Person>> Rules => [
        ..This()
            .Should(parent => parent.Age < parent.HeightInCm),
        ..Property(e => e.Name)
            .NotNullOrEmpty()
            .Length(2..32)
            .Matches(@"\w+"),
        ..Property(e => e.Age)
            .Min(18)
            .NotEqual(22),
        ..Property(e => e.Nicknames)
            .DoesNotContain("Thinus")
            .When(parent => parent.Name == "Thijn")
    ];
}