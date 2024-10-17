# Bouwinstructies voor FactorX

Deze instructies helpen je bij het opzetten van de ontwikkelomgeving voor FactorX en het bouwen van het project.

## Vereisten

- Visual Studio 2022 of hoger
- .NET Framework 4.8 SDK
- Git

## Stappen

1. Clone de repository:   ```
   git clone https://github.com/yourusername/factorx.git   ```

2. Open de solution file `FactorX.sln` in Visual Studio.

3. Herstel de NuGet-pakketten door rechts te klikken op de solution in de Solution Explorer en "Restore NuGet Packages" te selecteren.

4. Build de solution door te gaan naar "Build" > "Build Solution" in de menubalk.

5. Start de applicatie in debug-modus door op F5 te drukken of ga naar "Debug" > "Start Debugging".

## Projectstructuur

- `FactorX.Core`: Bevat de kernlogica en modellen
- `FactorX.Data`: Bevat de data-toegangslaag en repositories
- `FactorX.UI`: Bevat de WPF-gebruikersinterface
- `FactorX.Tests`: Bevat unit tests voor het project

## Tips voor ontwikkelaars

- Gebruik de MVVM (Model-View-ViewModel) architectuur voor een schone scheiding van logica en UI.
- Schrijf unit tests voor alle belangrijke functionaliteiten.
- Houd je aan de C# codeerstandaarden en naamgevingsconventies.
- Gebruik asynchrone programmering waar mogelijk voor een responsieve UI.

