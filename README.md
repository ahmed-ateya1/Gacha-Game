# 🎮 Gacha Game

This project is a **Gacha Game** system built with **ASP.NET Core Web API**. It handles player management, gacha pulls, character inventory, and banner mechanics, simulating a complete gacha experience.

## 📌 Features

- 🔐 User Authentication (JWT + Refresh Token, Identity)
- 🎁 Gacha Pull System (Banner-based random character draws)
- 🎮 Character and Inventory Management
- 🌟 Rarity and Drop Rate Mechanics
- 📊 Pull Results and History Tracking
- 📬 Email Notifications via SMTP
- 🧰 Admin support for adding banners and characters
- 📦 CQRS with MediatR for clean and scalable command-query separation
- ✅ Input validation using FluentValidation
- 🧭 Mapping with Mapster
- 📜 Structured Logging with Serilog

## 🧱 Entity Overview

| Entity | Description |
|--------|-------------|
| **Player** | Stores user login credentials, balance info, etc. |
| **Character** | Core character entity with base stats, rarity, and element. |
| **ElementType** | Represents elemental types (e.g., Fire, Water). |
| **Rarity** | Defines rarity levels and drop rates. |
| **GachaBanner** | Time-limited banners for specific characters. |
| **BannerCharacter** | Many-to-many link between banners and characters. |
| **GachaPulls** | Pull session including user, banner, and cost. |
| **PullResult** | Characters obtained from a gacha pull. |
| **UserCharacters** | Inventory of characters owned by players. |

📌 **Database ERD**

![image](https://github.com/user-attachments/assets/43516df5-c869-4d90-8e8c-9972c9ee6553)

## 🧰 Tech Stack

- **Framework**: ASP.NET Core Web API
- **Database**: SQL Server + EF Core (Code-First)
- **Auth**: JWT + Refresh Tokens, ASP.NET Identity
- **Validation**: FluentValidation
- **Mapping**: Mapster
- **Logging**: Serilog
- **Architecture**: CQRS + MediatR
- **Email**: SMTP

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/ahmed-ateya1/gacha-game.git
   cd gacha-game
