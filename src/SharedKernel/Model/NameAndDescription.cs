namespace SharedKernel.Model;

public readonly record struct NameAndDescription(
    string Name,
    string? Description)
{
    // TODO: we should add some nicer ctor(s) here
    //public NameAndDescription(string nameCommaDescription)
    //{
    //}
}
