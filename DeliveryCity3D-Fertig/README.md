# Delivery City 3D

Version **2.0.0** · Windows 64 Bit · Unity **6000.6.5f1**

Ein 3D-Lieferspiel für Windows, entwickelt mit Unity und C#. Wähle das Paketlager und danach ein Lieferziel. Ein freier Wagen lädt das Paket, fährt über die Straßen zum Gebäude und kehrt anschließend zu seinem Parkplatz zurück.

## Sofort spielen

1. Den Ordner `DeliveryCity3D-Spiel` öffnen. Bei einer ZIP-Datei zuerst **alles entpacken**.
2. `SPIEL STARTEN.cmd` doppelt anklicken. Diese Startdatei öffnet das Spiel mit der Vollbild-Einstellung.
3. Eine Stadt auswählen und **Schicht starten** drücken.
4. Das orange **LAGER** in der Stadt anklicken.
5. Ein markiertes Gebäude oder seinen Namen unter **Lieferziele** anklicken.
6. Warten, bis ein Wagen wieder **Bereit** ist. Dann den nächsten Auftrag vergeben.

**F11** schaltet zwischen Fenster und Vollbild um. **Esc** öffnet die Pause. Zum Spielen wird Visual Studio nicht benötigt.

Alternativ lässt sich `DeliveryCity3D.exe` direkt öffnen. Die Startdatei `SPIEL STARTEN.cmd` liegt im selben Ordner und verwendet diese Anwendung.

Der Lieferzähler steigt erst, wenn das Paket am Haus abgegeben wurde. Während der Rückfahrt ist der Wagen noch beschäftigt.

## Zwei Städte

| Stadt | Fahrzeuge | Lieferziel | Zeit |
|---|---:|---:|---:|
| Little Oak | 1 | 6 Pakete | 4 Minuten |
| Harbor Side | 2 | 10 Pakete | 4 Minuten |

Beide Städte sind im Menü auswählbar. In Harbor Side können zwei Lieferungen gleichzeitig laufen. Das Spiel wählt den freien Wagen automatisch aus.

Die Städte werden mit einer perspektivischen 3D-Kamera gezeigt. Harbor Side ergänzt die Stadt um einen kleinen Hafenbereich. Die Kameraansicht ist fest eingestellt, damit sich die Bedienung auf die Lieferaufträge konzentriert.

## Projekt in Unity öffnen

1. Unity Hub öffnen und ein **vorhandenes Projekt hinzufügen**.
2. Den Ordner `DeliveryCity3D-Fertig` auswählen. Er enthält `Assets`, `Packages` und `ProjectSettings`.
3. Unity **6000.6.5f1** verwenden; die Version steht auch in `ProjectSettings/ProjectVersion.txt`.
4. Im Project-Fenster `Assets/Scenes/LittleOakGame.unity` doppelt anklicken.
5. Oben auf **Play** klicken. Zum Beenden erneut auf den Play-Knopf klicken.

Die Gebäude, Straßen, Materialien, Kameras, Lichter und Menüs sind als gewöhnliche Unity-Objekte gespeichert. Sie können in der Szene und im Inspector bearbeitet werden. Visual Studio wird zum Lesen und Ändern der C#-Dateien verwendet.

## Vier überschaubare Spielskripte

| Datei unter `Assets/Scripts` | Aufgabe |
|---|---|
| `DeliveryStop.cs` | Daten eines Lagers oder Zielgebäudes |
| `DeliveryTruck.cs` | Fahrt, Laden, Entladen und Rückkehr |
| `DeliveryGame.cs` | Aufträge, Eingaben, Zeit und Punkte |
| `DeliveryHUD.cs` | Texte, Menüs und Schaltflächen |

Unity übernimmt die Darstellung und die Wegsuche. Die Skripte verbinden diese Funktionen mit den Spielregeln. Zum Verstehen braucht ihr vor allem Variablen, Methoden, Bedingungen, Schleifen und die sechs Fahrzeugzustände.

## Anleitung

- [Spielen und Unity-Aufbau](Anleitung/01-Benutzung-und-Aufbau.md)
- [Code Schritt für Schritt verstehen](Anleitung/02-Code-verstehen.md)
- [Aufgaben im Zweierteam und Abgabe](Anleitung/03-Team-und-Abgabe.md)
- [Testbericht](Anleitung/04-Testbericht.md)
- [Assets und Quellen](Anleitung/05-Assets-und-Quellen.md)

Der automatische Spieltest hat **2.127 Prüfbedingungen ohne Unity-Laufzeitfehler** bestanden. Beide Städte wurden mit den normalen Regeln gewonnen. Der Ordner `Vorschau` enthält **24 Ansichten** der beiden Städte bei drei Bildgrößen. Umfang und Grenzen der Prüfung stehen im Testbericht.

Zum Weitergeben immer den **gesamten Windows-Spielordner** verwenden: Die EXE benötigt die danebenliegenden Daten und Bibliotheken. Das bearbeitbare Unity-Projekt wird separat weitergegeben.


