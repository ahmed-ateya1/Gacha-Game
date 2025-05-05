Gacha-Game 
Gacha-Game is a web-based gacha simulation platform that allows players to engage with gacha banners, pull characters, and manage their inventories. Players can explore various characters with different elemental types and rarities, and track their pull history. The system incorporates JWT authentication and email notifications for an enhanced gaming experience.
Features

User Authentication: Secure login and registration with Identity and SMTP support (email confirmation, forgot password, reset password).
Gacha Pull Management: Players can perform pulls on gacha banners.
Inventory Management: Players can view and manage their character collections.
Pull History: Tracks all gacha pull results for players.
Validation: Fluent Validation ensures data integrity.
CQRS Pattern: Implements Command and Query Responsibility Segregation for clean architecture.
Security: Utilizes JWT and Refresh Token for authentication.
Logging: Serilog for monitoring and debugging.

Tech Stack

Backend: ASP.NET Web API
Database: SQL Server
ORM: Entity Framework Core
Querying: LINQ
Validation: Fluent Validation
Logging: Serilog
Object Mapping: Mapster
Security: JWT & Refresh Token, Identity
Email: SMTP
Architecture: CQRS & MediatR

Database Schema
The core entities in Gacha-Game include:

Player: Represents the game users with authentication details.
BannerCharacter: Links characters to specific gacha banners.
Character: Stores character details including element type and rarity.
ElementType: Enum for character elemental types.
Rarity: Enum for character rarity levels.
GachaBanner: Represents a gacha banner with associated characters.
Inventory: Manages a player's character collection.
GachaPulls: Tracks a player's pull history.
PullResult: Represents the outcome of a gacha pull.

Relationships:

A Player can perform multiple GachaPulls.
A GachaBanner contains multiple BannerCharacters.
A Player owns an Inventory with multiple Characters.
A PullResult is generated from each GachaPull.

Installation & Setup

Clone the repository:git clone https://github.com/yourusername/Gacha-Game.git
cd Gacha-Game


Configure the database:
Set up SQL Server and update the connection string in appsettings.json in the Gacha-Game.API project.
Run migrations:dotnet ef migrations add InitialCreate --project Gacha-Game.Infrastructure
dotnet ef database update --project Gacha-Game.Infrastructure




Install dependencies:dotnet restore


Run the application:dotnet run --project Gacha-Game.API


Configure environment variables for Identity & SMTP (email services) and JWT settings.

Schema

License
This project is licensed under the MIT License.
