# 2. Den Code Schritt für Schritt verstehen

## Zuerst den Zusammenhang lernen

Alle Spielskripte liegen in `Assets/Scripts`. Öffnet sie durch Doppelklick in Unity.

| Skript | Eine Aufgabe |
|---|---|
| `DeliveryStop.cs` | Beschreibt ein Lager oder Zielgebäude |
| `DeliveryTruck.cs` | Führt eine Lieferung aus |
| `DeliveryGame.cs` | Verwaltet die Runde und ihre Aufträge |
| `DeliveryHUD.cs` | Zeigt die Werte an und verbindet Buttons |

Lest sie in dieser Reihenfolge. Gebäude, Straßen, Lichter und Menüs sind bereits in Unity gespeichert. Die Skripte müssen diese Objekte beim Spielen nicht erst erzeugen.

Unity berechnet auch den Fahrweg. Der C#-Code entscheidet, **wann** ein Wagen **wohin** fahren soll.

## Die wichtigsten C#-Bausteine

| Schreibweise | Bedeutung |
|---|---|
| `public string stopName` | Ein von außen erreichbarer Textwert, hier im Inspector einstellbar |
| `private float timer` | Eine interne Zahl mit Nachkommastellen |
| `bool` | Ein Wert, der nur wahr oder falsch sein kann |
| `if (...)` | Den folgenden Schritt nur bei erfüllter Bedingung ausführen |
| `return` | Die aktuelle Methode verlassen |
| `foreach (...)` | Alle Elemente einer Liste nacheinander ansehen |
| `null` | Es ist kein Objekt zugeordnet |
| `List<DeliveryStop>` | Eine veränderbare Liste von Lieferzielen |
| `Delivered { get; private set; }` | Andere Skripte dürfen den Wert lesen; nur dieses Skript darf ihn ändern |
| `=>` | Kurze Schreibweise für die Rückgabe eines Wertes |
| `Bedingung ? A : B` | Bei wahr den Wert A, sonst den Wert B verwenden |

Unity ruft `Awake()` beim Initialisieren einer Komponente, `Start()` vor deren erstem `Update()` und `Update()` anschließend in jedem Bild auf.

`Time.deltaTime` ist die vergangene Spielzeit seit dem letzten Bild. So läuft ein Timer unabhängig von der Bildrate.

## Schritt 1: DeliveryStop

Dieses Skript ist der Steckbrief eines Lieferortes:

- `stopName`: Wie heißt das Ziel?
- `isDepot`: Ist es das Lager?
- `arrivalPoint`: Wo hält der Wagen?
- `marker`: Welches sichtbare Schild wird hervorgehoben?

`Position` liefert die Position des Haltepunkts. `Highlight(true)` hebt einen offenen Auftrag farblich hervor. `Highlight(false)` stellt die ursprüngliche Markerfarbe wieder her. Das Lager behält seine eigene Farbe.

**Kleine Übung:** Ändert im Inspector einen Gebäudenamen. Der Fahrweg bleibt derselbe, aber die Anzeige verwendet den neuen Namen.

## Schritt 2: DeliveryTruck

Ein Auto befindet sich immer in genau einem von sechs Zuständen:

| Zustand im Code | Bedeutung |
|---|---|
| `Idle` | Bereit |
| `ToDepot` | Zum Lager fahren |
| `Loading` | Paket laden |
| `ToHouse` | Zum Haus fahren |
| `Unloading` | Paket abgeben |
| `Returning` | Zum Parkplatz zurückfahren |

Die Variable `state` merkt sich den aktuellen Schritt. `IsBusy` ist wahr, solange der Wagen nicht `Idle` ist. Deshalb ist er auch während der Rückfahrt beschäftigt.

### Einen Auftrag annehmen

Der Manager ruft `TryDeliver(depot, target, game)` auf. Der Wagen speichert das Lager, den Spielmanager und das Ziel in `AssignedTarget`. Anschließend startet `DriveTo()` die erste Fahrt.

`TryDeliver()` gibt `true` zurück, wenn der Auftrag angenommen wurde. Bei `false` bleibt er in der offenen Auftragsliste.

### Fahren und anhalten

`DriveTo()` verwendet den `NavMeshAgent`:

1. `SamplePosition` sucht eine gültige Position auf der Straße.
2. `CalculatePath` berechnet den Weg.
3. Nur ein vollständiger Weg wird mit `SetPath` übernommen.

Während der Fahrt prüft `Update()`, ob der Wagen angekommen und zum Stillstand gekommen ist. Danach wechselt `state` zum Laden oder Entladen. Beide Schritte dauern jeweils ungefähr 0,8 Sekunden. `ShowParcel()` blendet das Paket ein oder aus.

### Die Zustellung melden

Beim Entladen merkt sich der Wagen kurz das zugestellte Ziel, löscht seine Zuweisung und ruft den Spielmanager direkt auf:

```csharp
DeliveryStop deliveredStop = AssignedTarget;
ClearAssignment();
game.OnDelivered(deliveredStop);
```

`ClearAssignment()` setzt nur `AssignedTarget` auf `null`. Die Berechnung der Punkte erfolgt anschließend im Manager. Der Wagen startet danach die Rückfahrt.

### Einen Fehler behandeln

`Fail()` hält den Wagen an, blendet das Paket aus und setzt den Zustand auf `Idle`. Ein noch nicht zugestelltes Ziel bleibt kurz in `AssignedTarget`, damit der Manager den Auftrag zurückgeben kann. Das geschieht zum Beispiel bei einem unerreichbaren Ziel oder einer Fahrt, die länger als 60 Spielsekunden dauert.

`ResetTruck()` setzt einen Wagen für eine neue Runde auf seinen Parkplatz. Es aktiviert den Agenten erst, nachdem die Navigationsfläche geladen wurde. Geparkte Wagen haben Ausweichpriorität 0, damit vorbeifahrende Wagen ihnen ausweichen.

**Kleine Übung:** Findet die Wartezeit `0.8f` im Ladezustand. Ändert sie vorübergehend auf `2f` und beobachtet den Unterschied.

## Schritt 3: DeliveryGame

### Eine Runde vorbereiten

`StartGame()` setzt Zeit, Punkte, Paketzähler und Fahrzeugpositionen zurück. Danach füllt `FillOrders()` die Liste `Orders` mit bis zu drei zufälligen Zielen.

Die Methode berücksichtigt nur Ziele, die weder bereits offen sind noch gerade von einem Wagen beliefert werden. `IsAssigned()` schaut dazu mit einer einfachen Schleife bei jedem Wagen nach `AssignedTarget`.

### Einen Klick verarbeiten

`ReadMouseClick()` schickt einen unsichtbaren Strahl von der Kamera durch die angeklickte Bildschirmstelle. `Physics.Raycast` findet das getroffene Objekt. Besitzt dieses oder eines seiner Elternobjekte `DeliveryStop`, wird es an `SelectStop()` übergeben.

`SelectStop()` prüft nacheinander:

1. Läuft die Runde ohne Pause?
2. Wurde das Lager gewählt und gibt es einen freien Wagen?
3. Steht das anschließend angeklickte Ziel in `Orders`?
4. Kann der freie Wagen die Lieferung übernehmen?

Nach einer erfolgreichen Vergabe entfernt der Manager das Ziel aus `Orders` und füllt die Liste auf. Das Ziel bleibt während der Lieferung beim zuständigen Wagen gespeichert.

### Punkte vergeben

Der direkte Aufruf `OnDelivered(target)` erhöht Paketzähler und Punkte:

```csharp
Delivered++;
Score += 100;
```

`Delivered++` bedeutet: einen gelieferten Auftrag hinzuzählen. `Score += 100` bedeutet: 100 zum Punktestand addieren.

Ist das Lieferziel erreicht, beendet `EndShift(true)` die Schicht erfolgreich. Bei abgelaufener Zeit beendet `EndShift(false)` die Runde. Eine erfolgreiche Runde erhält zusätzlich Restzeitbonus und Sterne. `PlayerPrefs` speichert den besten Punktestand der jeweiligen Stadt auf diesem Rechner.

### Fehlgeschlagene Aufträge zurückgeben

`ReturnFailedOrders()` sucht einen freien Wagen mit einem noch eingetragenen `AssignedTarget`. Das bedeutet: Die Fahrt wurde abgebrochen. Der Manager stellt das Ziel zurück in `Orders` und leert danach die Zuweisung des Wagens.

### Pause und Wechsel

`TogglePause()` setzt `Time.timeScale` auf 0 oder 1. Bei 0 stehen Spielzeit und Fahrten still. `Restart()` beginnt eine neue Runde. `LoadCity()` lädt die ausgewählte Stadtszene.

**Kleine Übung:** Ändert im Inspector `shiftDuration` kurz auf 30. Beobachtet das Rundenende und stellt den Wert danach wieder auf 240.

## Schritt 4: DeliveryHUD

Die Oberfläche liest die Werte aus `DeliveryGame`. Sie bestimmt keine Aufträge und vergibt keine Punkte.

`Start()` verbindet jeden Button einmal mit seiner Aktion. Beispielsweise bedeutet:

```csharp
startButton.onClick.AddListener(game.StartGame);
```

Wenn der Startbutton angeklickt wird, soll `StartGame()` ausgeführt werden.

`Refresh()` aktualisiert in jedem Bild die Texte, den Fortschrittsbalken und die sichtbaren Bereiche:

| Spielzustand | Anzeige |
|---|---|
| Vor dem Start | Hauptmenü |
| Laufende Runde | Lieferaufträge, Fahrzeugstatus, Restzeit und Hinweis |
| Pause | Pausefenster über der Spielanzeige |
| Runde beendet | Ergebnis mit Punkten, Sternen und Bestwert |

Die drei Auftragsbuttons rufen über `ChooseOrder()` ebenfalls `game.SelectStop()` auf. Damit gelten dieselben Regeln wie beim Klick auf ein Gebäude.

Für die Uhr wird die Restzeit auf ganze Sekunden aufgerundet. `seconds / 60` liefert Minuten; `seconds % 60` liefert die übrigen Sekunden. `ToString("00")` sorgt für zwei Stellen.

`ToggleFullscreen()` schaltet bei F11 oder beim entsprechenden Button zwischen Vollbild und Fenster um. Im Unity-Editor verwendet ihr stattdessen die Game-Ansicht; mit dem Mauszeiger darüber vergrößert **Shift + Leertaste** dieses Fenster.

## Eine Lieferung gemeinsam erklären

Person 1 erklärt den Wagen, Person 2 den Manager und die Oberfläche:

`Lager wählen → Ziel wählen → Auftrag an Wagen → Laden → Fahren → Entladen → Punkte → Rückfahrt`

Nennt zu jedem Schritt das zuständige Skript. Sobald ihr diese Kette erklären und eine kleine Änderung selbst prüfen könnt, ist der wichtigste Zusammenhang verstanden.

