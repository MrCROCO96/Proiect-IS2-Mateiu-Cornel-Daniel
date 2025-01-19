using System;
using System.Collections.Generic;

namespace GameTicketing.Database.Models;

public partial class Users
{
    public int Id { get; set; }

    public string Nume { get; set; } = null!;

    public string Prenume { get; set; } = null!;

    public string Functie { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public string Email { get; set; } = null!;
}