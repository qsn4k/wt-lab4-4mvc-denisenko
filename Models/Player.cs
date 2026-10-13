using System.ComponentModel.DataAnnotations;

namespace wt_lab4_4mvc_denisenko.Models;

public class Player
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите никнейм игрока.")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "Никнейм должен содержать от 2 до 30 символов.")]
    [Display(Name = "Никнейм")]
    public string Nickname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите название игры.")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "Название игры должно содержать от 2 до 60 символов.")]
    [Display(Name = "Игра")]
    public string Game { get; set; } = string.Empty;

    [Range(0, 5000, ErrorMessage = "Рейтинг должен находиться в диапазоне от 0 до 5000.")]
    [Display(Name = "Рейтинг")]
    public int Rating { get; set; }

    [Range(0, 100000, ErrorMessage = "Количество побед не может быть отрицательным.")]
    [Display(Name = "Победы")]
    public int Wins { get; set; }

    [Range(0, 100000, ErrorMessage = "Количество поражений не может быть отрицательным.")]
    [Display(Name = "Поражения")]
    public int Losses { get; set; }

    [Display(Name = "Дата регистрации")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public double WinRate => Wins + Losses == 0
        ? 0
        : Wins * 100.0 / (Wins + Losses);
}
