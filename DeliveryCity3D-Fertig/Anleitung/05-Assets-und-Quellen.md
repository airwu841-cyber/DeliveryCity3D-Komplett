# 5. Assets und Quellen

Diese Übersicht nennt die vorhandenen Quellen und Lizenzbelege. Unbekannte Herkunftsangaben sind ausdrücklich als offen gekennzeichnet.

## Spielinhalte

| Inhalt | Herkunft und Lizenzstand |
|---|---|
| Modelle unter `Assets/Models/Roads` und `Assets/Models/Trucks`, einschließlich ihrer `colormap.png`-Texturen | Aus dem bereitgestellten Ausgangsprojekt übernommen. Die Modellordner enthalten 145 FBX-Dateien. Ein ursprünglicher Downloadlink, Urheber oder zugehöriger Lizenztext wurde dort nicht gefunden. |
| Stadtobjekte, Dach- und Baummeshes, Materialien und Schilder unter `Assets/DeliveryTown` | Aus dem bereitgestellten Stadtstand übernommen und für die Spielszenen weiterverwendet. Die Dateien sind gewöhnliche Unity-Objekte und Projektressourcen. Daraus ergibt sich keine zusätzliche Lizenzfreigabe für importierte Modelle. |
| `Assets/Scenes/LittleOakGame.unity`, `HarborSide.unity` und `Assets/Scripts` | Spielszenen und Spiellogik dieser Projektfassung; sie verbinden Stadtobjekte, Navigation, Lieferaufträge und Oberfläche. |
| `Assets/Audio/Select.wav` und `Delivered.wav` | Für das Projekt aus Sinusschwingungen und einer kurzen Lautstärkehüllkurve erzeugte Signaltöne. Keine übernommenen Musikaufnahmen. |
| Oberfläche: Inter, gespeichert als `Assets/DeliveryCity/Fonts/Interface.ttf` | Copyright 2020 The Inter Project Authors. Quelle: [Google Fonts – Inter](https://github.com/google/fonts/tree/main/ofl/inter). SIL Open Font License 1.1; vollständiger Text in [OFL.txt](../Assets/DeliveryCity/Fonts/OFL.txt). |
| Weltbeschriftung und Ersatzschrift `LegacyRuntime.ttf` | In Unity eingebaute Schriftressource. |

Die FBX-Metadaten nennen **Unity FBX Exporter 5.0.0** als speicherndes Programm. Das bezeichnet das Exportwerkzeug und belegt nicht den Urheber der Modelle.

Für die übernommenen Straßen- und Fahrzeugmodelle sollte der ursprüngliche Quellenbeleg ergänzt werden, sobald der Download oder Asseteintrag verfügbar ist: **Paketname, Urheber, direkte Quellseite und mitgelieferte Lizenzdatei**. Eine Modellähnlichkeit oder ein Dateiname allein ist kein verlässlicher Lizenznachweis.

Die Inter-Schrift wird mit ihrem vollständigen Lizenztext weitergegeben. Beim Weitergeben des Projekts diesen Text bei der Schrift belassen.

## Verwendete Unity-Pakete

Die Versionen sind in `Packages/manifest.json` festgehalten. Für die folgenden Pakete liegt in der installierten Paketkopie jeweils ein `LICENSE.md` mit der **Unity Companion License for Unity-dependent projects** vor.

| Paket | Version | Funktion |
|---|---|---|
| AI Navigation | 2.0.16 | Straßen-Navigationsfläche und Fahrzeugwegfindung |
| Input System | 1.20.0 | Maus- und Tastatureingaben |
| Universal Render Pipeline | 17.6.0 | 3D-Darstellung und Materialien |
| Unity UI / uGUI | 2.6.0 | Texte, Panels und Schaltflächen |

Die Lizenzdateien liegen nach dem Import unter `Library/PackageCache/<Paketordner>/LICENSE.md`. Unity erzeugt `Library` beim Öffnen des Projekts neu. Die Paketlizenzen sind keine Herkunftsangabe für die getrennt übernommenen FBX-Modelle.

## Kursunterlagen und Lernquellen

Die Abgabe- und Präsentationsanforderungen stammen aus dem bereitgestellten `Organization.md`.

Das bereitgestellte `Unity.md` nennt [Unity Learn](https://learn.unity.com/) und den [Brackeys-Kanal](https://www.youtube.com/@Brackeys/featured) als ergänzende Lernquellen. Sie sind hier als Lernangebote aufgeführt, nicht als Nachweis für übernommenen Quellcode.

Für zusätzliche verwendete Modelle, Texte, Geräusche oder Codebeispiele ergänzt ihr diese Übersicht um die tatsächliche Quelle und die zugehörigen Lizenzangaben.

