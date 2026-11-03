# Übung OrderShop – Composition, DIP, ISP und Constructor Injection

**Lehrjahr:** 3./4. Lehrjahr  ·  **Bezug:** Auftrag 2, 3 und 4  ·  **Dauer:** ca. 1–2 Tage

## Ausgangslage

Du übernimmst den Code des kleinen Webshops **OrderShop**. Er funktioniert, aber das Team hat Mühe:

- Neue Benachrichtigungsarten sind aufwendig.
- Der `OrderService` lässt sich nicht testen.
- Wer Kundendaten nur anzeigen will, bekommt trotzdem Zugriff auf Löschen und Mailversand.

Deine Aufgabe ist es, den Code so umzubauen, dass er testbar und erweiterbar wird, **ohne das fachliche Verhalten zu verändern**.

## Projektstruktur

```
OrderShop.sln
├── src/
│   ├── OrderShop.Core            Fachlogik (Bestellungen, Kunden, Benachrichtigungen)
│   ├── OrderShop.Infrastructure  Technische Details (simulierte Datenbank, SMTP, SMS)
│   └── OrderShop.App             Konsolenanwendung
└── tests/
    └── OrderShop.Tests           xUnit-Tests
```

Starten und testen:

```bash
dotnet run --project src/OrderShop.App
dotnet test
```

Beim ersten `dotnet test` ist **ein Test rot** und **neun Tests sind übersprungen**. Das ist Absicht.

## Geschäftsregeln (dürfen sich nicht ändern)

1. Nur bestehende, aktive Kunden dürfen bestellen.
2. Eine Bestellung braucht mindestens eine Position.
3. Ab einem Warenwert von CHF 200.00 gibt es 10 % Rabatt.
4. Am Wochenende (Samstag/Sonntag) gibt es zusätzlich 5 % Rabatt auf den Warenwert.
5. Der Endbetrag wird auf 5 Rappen gerundet.
6. Nach dem Speichern erhält der Kunde eine Bestellbestätigung.

## Spielregeln

- Kein Mocking-Framework (Moq, NSubstitute, …). Schreibe Testdoubles von Hand.
- Die Geschäftsregeln und die Ausgabe der Konsolenanwendung bleiben fachlich gleich.
- Arbeite in kleinen Schritten und committe nach jedem Teil.

---

## Teil A – Analyse (vor dem Programmieren)

Lies den Code und halte deine Antworten schriftlich fest.

1. Zeichne die Projektreferenzen zwischen Core, Infrastructure, App und Tests auf. In welche Richtung zeigt die Compile-Time-Abhängigkeit zwischen Core und Infrastructure?
2. Liste alle Stellen in `OrderShop.Core`, an denen mit `new` eine Abhängigkeit erzeugt oder statischer Zustand bzw. `DateTime.Now` verwendet wird.
3. Warum sind die Tests in `OrderServiceTests` übersprungen? Nenne für jede Abhängigkeit des `OrderService` den konkreten Grund.
4. Welche Klassen im Core gehören zur **High-Level-Policy**, welche Teile sind **Low-Level-Details**?
5. Schau dir `FakeCustomerService` in den Tests an. Wie viele Methoden werden wirklich gebraucht? Was sagt das über `ICustomerService` aus?

## Teil B – Composition statt starrer Hierarchie (Auftrag 2)

Datei: `Core/Notifications/Notifications.cs`

Der Product Owner wünscht zusätzlich den Kanal **Push** und das Format **HTML**.

1. Wie viele Klassen bräuchte die heutige Hierarchie nach dieser Anforderung? Welcher Code wäre dupliziert?
2. **Leitfrage:** Welche Eigenschaften sollen unabhängig variieren?
3. Baue die Benachrichtigungen so um, dass Kanal und Format unabhängig kombiniert werden können. Das zusammengesetzte Objekt delegiert an die enthaltenen Objekte und bekommt sie per **Constructor Injection**.
4. Ergänze Push und HTML. Zähle die Klassen vorher und nachher.
5. Nenne eine Alternative zu deiner Lösung und ihre Kosten.
6. Ist jede Kombination fachlich sinnvoll? Wie gehst du damit um?

## Teil C – Dependency Inversion und Constructor Injection (Auftrag 3)

Dateien: `Core/Ordering/OrderService.cs`, `Infrastructure/PostgreSqlOrderRepository.cs`

1. **Leitfrage:** Besitzt die Fachlogik die benötigte Abstraktion, oder diktiert die Infrastruktur den Vertrag? Begründe am Beispiel von `ExecuteInsert` und `ExecuteSelectById`.
2. Entwirf eine Abstraktion für die Bestellungsablage **aus Sicht der Fachlogik**. Welche Methoden braucht der `OrderService` wirklich? Welche Begriffe dürfen darin **nicht** vorkommen?
3. Entscheide, in welchem Projekt die Abstraktion liegt, und begründe.
4. Kehre die Projektreferenz um, sodass `ArchitectureTests.Core_DoesNotReference_Infrastructure` grün wird.
5. Mache den `OrderService` testbar: **Alle** Abhängigkeiten kommen über den Konstruktor. Denk auch an die Uhrzeit.
6. Woher kommt der Connection String nach deinem Umbau?
7. Erkläre den Unterschied zwischen DIP und Dependency Injection. Gibt es in deiner Lösung eine Stelle, an der du Constructor Injection verwendest, ohne dass dahinter ein Interface steht?
8. **Grenze:** Nicht jede konkrete Klasse benötigt ein Interface. Nenne eine Klasse in deiner Lösung, die bewusst **kein** Interface hat, und begründe.

## Teil D – Sinnvolle Interfaces und ISP (Auftrag 4)

Dateien: `Core/Customers/CustomerService.cs`, `Core/Customers/CustomerClients.cs`

1. Erstelle eine Tabelle: Welcher Client (`CustomerDetailView`, `CustomerReportJob`, `OrderService`, App) benutzt welche Methoden von `ICustomerService`?
2. Entwirf rollenbezogene Client-Verträge.
3. Welche Schnitte hast du **verworfen** und warum? (Mindestens einen nennen.)
4. Gehört der CSV-Export und der Mailversand überhaupt in einen Kunden-Datenzugriff? Wohin verschiebst du sie?
5. Kann eine Klasse mehrere deiner neuen Interfaces implementieren? Was bedeutet das für die Registrierung im DI-Container (Teil E)?
6. Vergleiche den Aufwand für die Testdoubles vorher und nachher.
7. **Grenze:** Ab wann wird es zu einer Interface-Explosion? Woran erkennst du das in deinem Code?

## Teil E – Composition Root

1. Verbinde in `OrderShop.App` alle Teile mit `Microsoft.Extensions.DependencyInjection`. Nur hier dürfen Infrastruktur-Klassen konkret vorkommen.
2. Wähle für jede Registrierung eine Lebensdauer (Singleton, Scoped, Transient) und begründe sie in einem Kommentar.
3. Was passiert, wenn ein Singleton einen Scoped-Service als Abhängigkeit hat? Wie kannst du das beim Start automatisch erkennen lassen?
4. Die Konsolenanwendung soll danach dieselbe fachliche Ausgabe wie vorher erzeugen.

## Teil F – Tests

1. Entferne das `Skip` bei allen Tests in `OrderServiceTests` und implementiere sie mit eigenen Testdoubles. Die Tests dürfen **nicht** vom aktuellen Wochentag abhängen.
2. Schreibe Tests für die Benachrichtigungen, ohne die Konsolenausgabe mitzulesen.
3. Passe `CustomerDetailViewTests` und `CustomerReportJobTests` an deine neuen Verträge an.
4. Schreibe mindestens einen Test, der deine DI-Konfiguration aus Teil E prüft.

## Akzeptanzkriterien

- [ ] `dotnet test`: alle Tests grün, kein Test übersprungen
- [ ] `ArchitectureTests.Core_DoesNotReference_Infrastructure` ist grün
- [ ] Im Core gibt es kein `new` auf Infrastruktur-Klassen, kein `DateTime.Now` und keinen statischen Zustand
- [ ] Push und HTML sind ergänzt, ohne bestehende Kanal-/Format-Klassen zu ändern
- [ ] Jeder Client kennt nur den Vertrag, den er wirklich braucht
- [ ] Die Antworten zu Teil A–E sind schriftlich festgehalten

## Reflexion (Abschluss, ca. ½ Seite)

1. An welcher Stelle hat dein Umbau den Code komplizierter gemacht? Lohnt sich das?
2. Welche der drei Ideen (Composition, DIP, ISP) hatte den grössten Einfluss auf die Testbarkeit?
3. Was würdest du in einem echten Projekt anders machen?
