# CoCBot

![Build Status](https://github.com/pajus1337/CoCBot/actions/workflows/build.yml/badge.svg?branch=dev)

**CoCBot** is a modular, automation-ready bot framework designed for games like Clash of Clans. The project focuses on clean architecture, scalability, and developer collaboration from the ground up.

---

## 🧱 Architecture

- **WinUI 3** frontend (cross-platform-ready via WinAppSDK)
- **Service-oriented backend** using Dependency Injection (Microsoft.Extensions.DependencyInjection)
- **Emgu.CV** for image recognition & screen interaction
- **Domain-driven structure**: Interfaces, Services, Controllers, Configuration, Models

---

## ✨ Features (Implemented)

- Emulator selector via ADB (user can pick device to control)
- Template-matching with image-based screen recognition
- Automated input (mouse click via Win32 API)
- First feature: Invite player to clan (steps automated)
- Logging support (to `Logs/bot.log`)

---

## 🛠 Technologies

- .NET 9.0 (Windows)
- Emgu.CV 4.11+
- WinUI 3 + Desktop SDK
- Microsoft.Extensions.\* (DI, Options)

---

## 🗂 Project Structure

```
CoCBotProject/
├── CoCBot/                 # Core bot logic
│   ├── Interfaces/         # Contracts for DI
│   ├── Services/           # Business logic
│   ├── Controllers/        # Flow & state manager
│   ├── Configurations/     # Option classes
│   ├── Assets/             # Template images
│   └── DependencyInjection.cs
├── CoCBot.UI/              # WinUI frontend
│   └── MainWindow.xaml
├── Logs/                  # Runtime logs
└── README.md
```

---

## 📦 Installation

1. Clone the repository
2. Open in Visual Studio 2022+
3. Build the solution
4. Set `CoCBot.UI` as startup project

---

## 🔁 Branch Strategy

| Branch     | Purpose                           |
| ---------- | --------------------------------- |
| `template` | Clean base project for reuse      |
| `dev`      | All contributions should go here  |
| `master`   | Production-ready, stable versions |

---

## 🔮 Planned / TODO

- Loop through player list in each clan
- Add conditional invite rules (e.g., min level, trophies)
- Scroll support for long lists
- Custom settings/config via UI
- Full multi-clan cycle
- Advanced logging/telemetry

---

## 🤝 Contributing

See [CONTRIBUTING.md](./CONTRIBUTING.md) for full guidelines and commit format.

---

## 📄 License

This project is licensed under the **GNU GPLv3**. See [LICENSE](./LICENSE) for details.

