using System;
using System.Collections.Generic;

namespace GameTicketing.Database.Models;

public partial class Category
{
    public int Id { get; set; }
    
    public string FunctiePersoana { get; set; } = null!;

    public string TipTichet { get; set; } = null!;

}