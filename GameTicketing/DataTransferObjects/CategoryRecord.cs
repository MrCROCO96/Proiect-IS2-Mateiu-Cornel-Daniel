﻿namespace GameTicketing.DataTransferObjects;

public class CategoryRecord
{
    public int Id { get; set; }
    public string FunctiePersoana { get; set; } = null!;
    public string TipTichet { get; set; } = null!;
}