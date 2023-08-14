﻿namespace SharedKernel.Abstractions;

public interface IUpdateableEntity : IEntity
{
    public DateTime CreatedOn { get; }
    public DateTime? UpdatedOn { get; }

    public void SetUpdatedOn();
}