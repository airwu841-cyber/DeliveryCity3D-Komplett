# 1. Spielen und den Unity-Aufbau verstehen

## Schritt 1: Das Spiel starten

Öffnet im Ordner `DeliveryCity3D-Spiel` die Datei **SPIEL STARTEN.cmd**. Sie startet die Anwendung `DeliveryCity3D.exe` mit der Vollbild-Einstellung. Wählt **Little Oak** und anschließend **Schicht starten**. Mit **F11** wechselt ihr zwischen Vollbild und Fenster. Im Pausemenü gibt es ebenfalls eine Schaltfläche für Vollbild.

Diese Anleitung beschreibt Spielversion **2.0.0**. Bei einer ZIP-Datei entpackt ihr zuerst den gesamten Ordner. Die EXE kann auch direkt per Doppelklick geöffnet werden.

Zum Testen in Unity öffnet ihr das Projekt `DeliveryCity3D-Fertig`, dann `Assets/Scenes/LittleOakGame.unity` und drückt oben **Play**. Die fertige Windows-Anwendung ist zum normalen Spielen bequemer. Visual Studio dient zum Bearbeiten des Codes.

## Schritt 2: Das erste Paket liefern

1. Lest links die drei **Lieferziele**.
2. Klickt auf das orange **LAGER** in der Stadt.
3. Klickt ein markiertes Zielgebäude an. Alternativ könnt ihr seinen Namen in der Auftragsliste anklicken.
4. Der freie Wagen nimmt den Auftrag an und fährt zum Lager.
5. Er lädt das Paket, fährt zum Ziel und entlädt es. Jetzt steigt der Paketzähler.
6. Er fährt zu seinem Parkplatz zurück. Sobald **Bereit** erscheint, kann er den nächsten Auftrag annehmen.

Für jeden Auftrag gilt erneut: **Lager → Ziel**. Ein Klick auf ein Haus ohne aktuellen Auftrag vergibt keine Lieferung. Der Hinweis im Spiel erklärt, was als Nächstes möglich ist.

Ein aus der Liste verschwundener Auftrag wurde bereits an einen Wagen vergeben. Das Paket zählt erst nach der Zustellung als geliefert.

## Schritt 3: Eine Schicht abschließen

| Stadt | Wagen | Benötigte Pakete | Zeitlimit |
|---|---:|---:|---:|
| Little Oak | 1 | 6 | 240 Sekunden |
| Harbor Side | 2 | 10 | 240 Sekunden |

In Harbor Side könnt ihr einen zweiten Auftrag vergeben, während der erste Wagen fährt. Das Spiel sucht automatisch den ersten freien Wagen.

Eine rechtzeitig abgeschlossene Schicht bringt Punkte und Sterne. Für jede Lieferung gibt es **100 Punkte**, für jede aufgerundete verbleibende Sekunde zusätzlich **5 Punkte**. Mindestens 40 % Restzeit ergeben drei Sterne, mindestens 15 % zwei Sterne, ansonsten einen Stern. Der beste erfolgreiche Punktestand jeder Stadt wird lokal gespeichert.

**Esc** oder **Pause** hält die Runde an. **Weiterliefern** führt sie weiter; **Schicht neu starten** setzt Fahrzeuge, Zeit, Aufträge und Punkte zurück. Über **Zur Stadtauswahl** kommt ihr ins Hauptmenü. Nach dem Sieg in Little Oak könnt ihr direkt zur zweiten Stadt wechseln.

## Schritt 4: Die wichtigsten Unity-Begriffe

| Begriff | Bedeutung |
|---|---|
| Szene | Eine gespeicherte Spielwelt; hier eine Stadt |
| GameObject | Ein Objekt, zum Beispiel Haus, Auto, Kamera oder Button |
| Komponente | Eine Funktion am Objekt, zum Beispiel Collider oder Skript |
| Inspector | Einstellungen des ausgewählten Objekts |
| Transform | Position, Drehung und Größe |
| Collider | Unsichtbare Form, die der Mausklick treffen kann |
| NavMesh | Zusammenhängende Fläche, auf der das Auto fahren darf |
| NavMeshAgent | Unity-Komponente, die das Auto auf dieser Fläche bewegt |
| Canvas | Träger der Texte, Menüs und Schaltflächen |

Die Szene enthält bereits die 3D-Welt und die Oberfläche. Zum Bearbeiten beendet ihr zuerst den Play-Modus, wählt ein Objekt in der **Hierarchy** aus und verändert es im **Inspector**. Speichert mit **Strg + S**. Änderungen während der Spielvorschau werden beim Stoppen meist zurückgesetzt.

## Schritt 5: Ein Liefergebäude ansehen

Wählt ein Zielgebäude aus und sucht die Komponente **Delivery Stop**:

| Feld | Aufgabe |
|---|---|
| `stopName` | Name in Auftragsliste und Nachricht |
| `isDepot` | Nur beim Paketlager einschalten |
| `arrivalPoint` | Haltepunkt des Wagens auf der Straße |
| `marker` | Sichtbare Markierung über dem Gebäude |

Das sichtbare Gebäude und sein Haltepunkt sind zwei verschiedene Dinge. Der Wagen hält vor dem Haus auf der Straße. Ein Collider am Gebäude oder einem Kindobjekt macht es anklickbar.

Probiert eine kleine Änderung: Ändert nur `stopName`, speichert die Szene und startet die Runde. Der neue Name erscheint in den Aufträgen, sobald dieses Ziel ausgewählt wird.

## Schritt 6: Einen Wagen und den Spielmanager ansehen

Der Wagen besitzt **Delivery Truck** und **NavMeshAgent**. `truckName` ist der Anzeigename. `parcelVisual` verweist auf das Paket, das während der Lieferung sichtbar wird.

Der Agent ist in der gespeicherten Szene zunächst ausgeschaltet. Das Skript aktiviert ihn beim Start, nachdem Unity die Navigationsfläche geladen hat. Das Einschalten von Hand ist deshalb nicht nötig.

Im Objekt mit **Delivery Game** findet ihr die Stadtwerte und die Verknüpfungen zu Lager, Zielgebäuden, Wagen und Parkplätzen. `trucks` und `parkingSpots` gehören in derselben Reihenfolge zusammen: erster Wagen zum ersten Parkplatz, zweiter Wagen zum zweiten.

Nach Änderungen an Straßen müsst ihr die Navigationsfläche mit der **NavMeshSurface** neu erzeugen. Kontrolliert dabei, dass Straßen verbunden sind und alle Haltepunkte darauf liegen.

## Schritt 7: Die Oberfläche bearbeiten

Unter **Delivery UI** liegen die gespeicherten Bereiche für Hauptmenü, Spielanzeige, Pause und Ergebnis. Ihr könnt Texte, Farben und Abstände im Inspector ändern.

**Delivery HUD** verbindet die vorhandenen Texte und Buttons mit dem Spiel. Diese Komponente bleibt auf dem aktiven Canvas. Die Buttons werden einmal beim Start durch das Skript verbunden; tragt dieselbe Aktion nicht zusätzlich im Inspector ein.

## Häufige Fragen

| Problem | Prüfen |
|---|---|
| In Unity erscheint eine leere Szene | `Assets/Scenes/LittleOakGame.unity` öffnen |
| Ein Auftrag startet nicht | Zuerst Lager wählen; ein Wagen muss **Bereit** sein |
| Ein Haus reagiert nicht | Aktueller Auftrag vorhanden? Collider und `DeliveryStop` vorhanden? |
| Ein Ziel ist unerreichbar | Haltepunkt auf einer verbundenen Navigationsfläche? |
| Ein Button reagiert nicht | `EventSystem`, `GraphicRaycaster` und HUD-Verknüpfung vorhanden? |
| Nach einer Codeänderung startet nichts | Rote Meldungen im Tab **Console** lesen und zuerst beheben |
| Spiel beenden wirkt im Unity-Editor nicht | Im Editor den Play-Knopf zum Stoppen verwenden |
| Die Windows-Anwendung startet nach dem Kopieren nicht | Den gesamten Spielordner kopieren, nicht nur die EXE |


