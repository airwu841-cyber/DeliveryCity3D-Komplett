# 4. Testbericht

Spielversion **2.0.0**, geprüft am **20. Oktober 2026** mit Unity **6000.6.5f1** unter Windows 64 Bit.

## Ergebnis

Der automatische Spieltest hat **2.127 Prüfbedingungen bestanden** und **0 Unity-Laufzeitfehler** aufgezeichnet. Little Oak und Harbor Side wurden mit ihren normalen Spielregeln erfolgreich abgeschlossen: 6 beziehungsweise 10 Pakete innerhalb von jeweils 240 Spielsekunden.

Die Zahl 2.127 bezeichnet einzelne geprüfte Bedingungen, keine unabhängigen Spielsitzungen. Das vollständige Ergebnis steht in [Testprotokoll.json](Testprotokoll.json).

## Geprüfte Funktionen

| Bereich | Ergebnis |
|---|---|
| Beide Städte | Szenen laden mit den richtigen Fahrzeugzahlen, Lieferzielen und Zeitlimits |
| Lieferablauf | Alle sieben Ziele je Stadt angefahren; Laden, Entladen, Punkte und Rückfahrt geprüft |
| Zwei Wagen | Gleichzeitige Lieferungen und Rückkehr zu getrennten Parkplätzen geprüft |
| Auswahl | Hausklick ohne Lagerwahl abgewiesen; Auftragsschaltflächen erst nach Lagerwahl freigegeben |
| Oberfläche | Buttons und Gebäudemarker durch Raycasts auf Sichtbarkeit und freie Klickflächen geprüft |
| Spielsteuerung | Start, Pause, Fortsetzen, Neustart, Ergebnis und Stadtwechsel geprüft |
| Fehlerfälle | Unerreichbares Ziel und blockierter Wagen geben den Auftrag wieder frei; keine falschen Lieferpunkte |
| Wertung | Lieferpunkte, Restzeitbonus, Sterne und gespeicherter Bestwert geprüft |
| Schrift und Anordnung | Importierte Schrift, Textflächen und Schaltflächen bei drei Bildgrößen geprüft |

## Darstellung

Für beide Städte wurden Hauptmenü, laufende Runde, Pause und Ergebnis bei **1600 × 900**, **1280 × 720** und **1920 × 1200** dargestellt. Daraus entstanden **24 Bilder** im Ordner `Vorschau`.

Die Bilder wurden aus den tatsächlichen Unity-Szenen mit ihrer Oberfläche gerendert. Die Skalierungsregeln des Canvas wurden für jede Bildgröße angewendet. In den anschließend visuell kontrollierten Ansichten waren Texte und Lieferziele lesbar und keine abgeschnittenen Beschriftungen oder verdeckten Liefermarker erkennbar.

## Testverfahren und Grenzen

Der Test steuerte die echten Fahrzeugzustände und die verbundenen Spielaktionen. Er führte keine künstlichen Lieferpunkte ein. Für einzelne Fehlerfälle wurden Werte vorübergehend verändert; die abschließenden Siegrunden verwendeten die veröffentlichten Regeln.

Viele Fahrten liefen mit beschleunigter Simulationszeit. Der gesamte Prüflauf dauerte etwa 60 reale Sekunden. Die 24 Ansichten wurden in separate Bildziele gerendert, nicht als Bildschirmaufnahmen eines sichtbaren Windows-Fensters erstellt.

F11, Vollbildschaltflächen und der Vollbildstarter sind implementiert. Der tatsächliche Wechsel des Windows-Fensters zwischen Vollbild und Fenstermodus wurde in diesem automatischen Prüflauf nicht ausgeführt.

Der Test wurde auf diesem Rechner durchgeführt. Vor der Präsentation sollten beide Teammitglieder selbst je eine vollständige Runde spielen und den vollständigen Spielordner auch auf einem zweiten Rechner starten.

## Auslieferung

Der abschließende Windows-Build wurde erfolgreich erstellt. Unity meldet **106.917.654 Bytes**, ungefähr **102 MiB** und damit deutlich unter 1 GB.

Die vier ausgelieferten Spielskripte und beide Szenen stimmen per SHA-256-Abgleich mit der erfolgreich geprüften Fassung überein. Zusätzliche Test- und Erstellungswerkzeuge liegen außerhalb des fertigen Unity-Projekts. Im Ordner `Assets/Scripts` befinden sich genau vier C#-Spielskripte.

Die Windows-Anwendung liegt unter `DeliveryCity3D-Spiel/DeliveryCity3D.exe`. `SPIEL STARTEN.cmd` öffnet sie mit der Vollbild-Einstellung. Beim Weitergeben muss der vollständige Spielordner zusammenbleiben.

