# Codekurs C#

Persönliche Lernsammlung zu C# und objektorientierter Programmierung. Das Repository verbindet eine verständliche Zusammenfassung mit praktischen Übungen, Aufgabenblättern und handgezeichneten Skizzen.

## Schnellzugriff

- [Zusammenfassung lesen](Zusammenfassung.md)
- [Übungen öffnen](Code_uebungen/)
- [Exceptions vertiefen](exceptions/)
- [Bilder ansehen](Bilder/)

## Themen und Aufgabenblätter

| Thema | Schwerpunkte | Unterlagen |
| --- | --- | --- |
| [Grundlagen](Code_uebungen/Grundlagen/) | Klassen, Objekte, Fields, Properties, Konstruktoren, Listen und Datentypen | [Zusammenfassung](Zusammenfassung.md) |
| [Vererbung](Code_uebungen/Vererbung/) | Basisklassen und abgeleitete Klassen | [Aufgabenblatt](Code_uebungen/Vererbung/aufgabe1-vererbung-michael%20%281%29.pdf) |
| [Polymorphie](Code_uebungen/Polymorphie/) | Unterschiedliches Verhalten mit `virtual` und `override` | [Aufgabenblatt](Code_uebungen/Polymorphie/aufgabe2-polymorphie-michael%20%281%29.pdf) |
| [Abstraktion](Code_uebungen/Abstraktion/) | Abstrakte Klassen und Methoden | [Aufgabenblatt](Code_uebungen/Abstraktion/aufgabe-abstraktion-michael%20%282%29.pdf) |
| [Interfaces](Code_uebungen/Interfaces/) | Schnittstellen und deren Implementierung | [Aufgabenblatt](Code_uebungen/Interfaces/aufgabe-interfaces-michael%20%281%29.pdf) |
| [Abstraktion und Interfaces](Code_uebungen/Abstraktion_und_Interfaces/) | Gemeinsame Fahrzeugübung | [Code](Code_uebungen/Abstraktion_und_Interfaces/FahrzeugProgramm.cs) · [Antworten](Code_uebungen/Abstraktion_und_Interfaces/antworten.txt) |
| [Fuhrpark](Code_uebungen/Fuhrpark/) | Zusammenspiel der OOP-Konzepte | [Aufgabenblatt](Code_uebungen/Fuhrpark/programmieraufgabe-fuhrpark-michael%20%281%29.pdf) · [Code](Code_uebungen/Fuhrpark/Program.cs) |
| [Exceptions](exceptions/) | Fehlerbehandlung und Fehlerweitergabe | [Zusammenfassung](Zusammenfassung.md#exceptions) · [Übungen](exceptions/) |

## Einstieg

1. Lies das gewünschte Thema in der [Zusammenfassung](Zusammenfassung.md).
2. Öffne das zugehörige Aufgabenblatt und die passende Übung.
3. Bearbeite und teste die Übung in einem eigenen C#-Konsolenprojekt oder einem C#-Onlinecompiler.
4. Vergleiche das Verhalten mit den Notizen und ergänze deine Erkenntnisse.

Die Sammlung enthält einzelne `.cs`-Dateien und derzeit keine `.csproj`- oder `.sln`-Datei. Viele Beispiele haben einen eigenen Programmeinstieg und verwenden gleiche Klassennamen. Führe deshalb jeweils eine Übung separat aus. Die Dateien dokumentieren Lernstände und können auch unvollständige Zwischenstände enthalten.

## Übungen im Detail

### Grundlagen

Die Ordner `ver1` und `ver2` enthalten unterschiedliche Übungsstände.

- [Value und Reference Types – erstes Beispiel](Code_uebungen/Grundlagen/ver1/Main.cs)
- [Kundenverwaltung – Stand 1](Code_uebungen/Grundlagen/ver1/Programm.cs)
- [Kundenverwaltung – Stand 2](Code_uebungen/Grundlagen/ver2/Programm.cs)
- [Value und Reference Types – weiteres Beispiel](Code_uebungen/Grundlagen/ver2/ValueType_ReferenceType.cs)

### Abstraktion und Interfaces

- [Fahrzeugprogramm](Code_uebungen/Abstraktion_und_Interfaces/FahrzeugProgramm.cs)
- [Antworten und Erkenntnisse](Code_uebungen/Abstraktion_und_Interfaces/antworten.txt)

### Fuhrpark

- [Programmcode](Code_uebungen/Fuhrpark/Program.cs)
- [Klassenhierarchie und Interfaces](Code_uebungen/Fuhrpark/Fuhrpark_Klassenhierarchie_und_Interfaces.jpeg)
- [Stack, Heap und Objektreferenzen](Code_uebungen/Fuhrpark/Fuhrpark_Stack_Heap_und_Objektreferenzen.jpeg)

### Exceptions

- [Schritt 2: Erstes try/catch](exceptions/Schritt_2_Erstes_Try_Catch.cs)
- [Schritt 5: finally](exceptions/Schritt_5_Finally.cs)
- [Schritt 7: Fehler wandert nach oben](exceptions/Schritt_7_Fehler_Wandert_Nach_Oben.cs)
- [Transferaufgabe: Bankkonto](exceptions/Transferaufgabe_Bankkonto.cs)
- [Skizze: Methodenaufrufe und Fehlerweitergabe](exceptions/Exceptions_Methodenaufrufe_und_Fehlerweitergabe.jpeg)

## Bilder zur Zusammenfassung

- [Deklaration einer Variablen](Bilder/skizze-deklaration.png)
- [Erstellen eines Objekts](Bilder/skizze-objekt.png)
- [Privates Field](Bilder/skizze-field.png)
- [Value Type](Bilder/value_type.png)
- [Reference Type](Bilder/reference_type.png)

## Ordnerstruktur

```text
codekurs-csharp/
├── README.md
├── Zusammenfassung.md
├── Bilder/
├── Code_uebungen/
│   ├── Grundlagen/
│   │   ├── ver1/
│   │   └── ver2/
│   ├── Vererbung/
│   ├── Polymorphie/
│   ├── Abstraktion/
│   ├── Interfaces/
│   ├── Abstraktion_und_Interfaces/
│   └── Fuhrpark/
└── exceptions/
```

## Sammlung erweitern

Lege neue Übungen im passenden Themenordner ab. Verwende aussagekräftige Dateinamen, ergänze die Erklärung in der Zusammenfassung und verlinke neue Unterlagen hier in der README. Halte Änderungen thematisch zusammen, damit sich der Lernfortschritt in der Git-Historie nachvollziehen lässt.
