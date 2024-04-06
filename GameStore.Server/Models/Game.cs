﻿using System.ComponentModel.DataAnnotations;

namespace GameStore.Server.Models;

public class Game
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    [Required]
    [StringLength(20)]
    public required string Genre { get; set; }
    [Required]
    [Range(1, 100)]
    public decimal Price { get; set; }
    [Range(1, 1000)]
    public int StockUnit { get; set; }
    [Required]
    public DateTime ReleaseDate { get; set; }
}