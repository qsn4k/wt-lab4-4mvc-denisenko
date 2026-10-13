using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_denisenko.Models;

namespace wt_lab4_4mvc_denisenko.Controllers;

public class PlayersController : Controller
{
    private static readonly List<Player> Players = new()
    {
        new Player
        {
            Id = 1,
            Nickname = "CyberWolf",
            Game = "Counter-Strike 2",
            Rating = 1500,
            Wins = 24,
            Losses = 12,
            CreatedAt = new DateTime(2026, 9, 1)
        },
        new Player
        {
            Id = 2,
            Nickname = "ShadowFox",
            Game = "Counter-Strike 2",
            Rating = 1750,
            Wins = 31,
            Losses = 10,
            CreatedAt = new DateTime(2026, 9, 2)
        },
        new Player
        {
            Id = 3,
            Nickname = "NeoMaster",
            Game = "Dota 2",
            Rating = 2100,
            Wins = 38,
            Losses = 9,
            CreatedAt = new DateTime(2026, 9, 3)
        },
        new Player
        {
            Id = 4,
            Nickname = "DarkKnight",
            Game = "Dota 2",
            Rating = 1850,
            Wins = 29,
            Losses = 15,
            CreatedAt = new DateTime(2026, 9, 4)
        },
        new Player
        {
            Id = 5,
            Nickname = "PixelHunter",
            Game = "Valorant",
            Rating = 1600,
            Wins = 21,
            Losses = 18,
            CreatedAt = new DateTime(2026, 9, 5)
        },
        new Player
        {
            Id = 6,
            Nickname = "IceStorm",
            Game = "Valorant",
            Rating = 1900,
            Wins = 34,
            Losses = 11,
            CreatedAt = new DateTime(2026, 9, 6)
        },
        new Player
        {
            Id = 7,
            Nickname = "RedDragon",
            Game = "Counter-Strike 2",
            Rating = 1250,
            Wins = 15,
            Losses = 20,
            CreatedAt = new DateTime(2026, 9, 7)
        }
    };

    public IActionResult Index(string? search, string? game)
    {
        IEnumerable<Player> result = Players;

        if (!string.IsNullOrWhiteSpace(search))
        {
            result = result.Where(player =>
                player.Nickname.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(game))
        {
            result = result.Where(player =>
                player.Game.Equals(game, StringComparison.OrdinalIgnoreCase));
        }

        ViewBag.Search = search;
        ViewBag.Game = game;
        ViewBag.Games = Players
            .Select(player => player.Game)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(item => item)
            .ToList();

        return View(result.OrderByDescending(player => player.Rating).ToList());
    }

    public IActionResult Details(int id)
    {
        Player? player = Players.FirstOrDefault(item => item.Id == id);

        if (player is null)
        {
            return NotFound();
        }

        return View(player);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Player());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Player player)
    {
        if (!ModelState.IsValid)
        {
            return View(player);
        }

        player.Id = Players.Count == 0 ? 1 : Players.Max(item => item.Id) + 1;
        player.CreatedAt = DateTime.Now;
        Players.Add(player);

        TempData["SuccessMessage"] = $"Игрок «{player.Nickname}» успешно добавлен.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Player? player = Players.FirstOrDefault(item => item.Id == id);

        if (player is null)
        {
            return NotFound();
        }

        return View(new Player
        {
            Id = player.Id,
            Nickname = player.Nickname,
            Game = player.Game,
            Rating = player.Rating,
            Wins = player.Wins,
            Losses = player.Losses,
            CreatedAt = player.CreatedAt
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Player model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        Player? player = Players.FirstOrDefault(item => item.Id == id);

        if (player is null)
        {
            return NotFound();
        }

        player.Nickname = model.Nickname;
        player.Game = model.Game;
        player.Rating = model.Rating;
        player.Wins = model.Wins;
        player.Losses = model.Losses;

        TempData["SuccessMessage"] = $"Данные игрока «{player.Nickname}» обновлены.";
        return RedirectToAction(nameof(Details), new { id = player.Id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        Player? player = Players.FirstOrDefault(item => item.Id == id);

        if (player is null)
        {
            return NotFound();
        }

        Players.Remove(player);
        TempData["SuccessMessage"] = $"Игрок «{player.Nickname}» удалён.";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Top()
    {
        List<Player> topPlayers = Players
            .OrderByDescending(player => player.Rating)
            .Take(3)
            .ToList();

        return View(topPlayers);
    }
}
