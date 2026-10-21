# 3. Aufgaben im Zweierteam und Abgabe

## Aufgaben aufteilen

| Person | Schwerpunkt | Unity-Bereiche und Dateien |
|---|---|---|
| Person 1 | Stadt und Fahrzeug | Straßen, Gebäude, Haltepunkte, Parkplätze, NavMesh, `DeliveryStop.cs`, `DeliveryTruck.cs` |
| Person 2 | Spielregeln und Oberfläche | Aufträge, Zeit, Punkte, Menüs, Canvas, `DeliveryGame.cs`, `DeliveryHUD.cs` |

Das ist eine vorgeschlagene Aufgabenverteilung. Haltet eure tatsächlich ausgeführten Änderungen und Beiträge fest. Beide Personen sollten den gesamten Lieferablauf erklären können.

## Person 1: Eine Lieferung technisch erklären

1. Ein Gebäude in Unity auswählen und Collider, Markierung und Haltepunkt zeigen.
2. Erklären, warum sichtbare Straße und Navigationsfläche unterschiedliche Dinge sind.
3. Einen Wagen auswählen und seine `NavMeshAgent`-Einstellungen zeigen.
4. Eine Lieferung starten und die sechs Fahrzeugzustände erklären.
5. Zeigen, wie das Paket beim Laden sichtbar und beim Entladen ausgeblendet wird.
6. Erklären, wie der Wagen eine abgeschlossene Lieferung an den Spielmanager meldet.

Eine passende technische Demonstration ist ein veränderter Haltepunkt in einer Testkopie der Szene. Zeigt, weshalb ein Haus ohne vollständigen Fahrweg nicht beliefert werden kann. Anschließend stellt ihr den gültigen Haltepunkt wieder her.

## Person 2: Eine Spielrunde technisch erklären

1. Die Reihenfolge Lager → Ziel und die Prüfung eines gültigen Auftrags zeigen.
2. Offene Aufträge von einem bereits vergebenen Fahrzeugziel unterscheiden.
3. Die Wahl eines freien Wagens und das Auffüllen auf drei offene Aufträge erklären.
4. Zeigen, wann Lieferpunkte, Zeitbonus und Sterne entstehen.
5. Die Verknüpfungen zwischen `DeliveryHUD`, Texten und Buttons im Inspector zeigen.
6. Pause, Neustart, Zeitablauf und Stadtwechsel vorführen.

Eine passende technische Demonstration sind zwei gleichzeitige Lieferungen in Harbor Side. Zeigt danach den Unterschied zwischen einer pausierten und einer neu gestarteten Runde.

## Gemeinsam üben

| Übung | Ergebnis |
|---|---|
| Einen Gebäudenamen ändern | Beide finden die passende Inspector-Einstellung |
| Eine Lieferung erklären | Beide kennen den Weg vom Klick bis zum Punktestand |
| Eine Ladezeit ändern | Beide finden die zuständige Bedingung im Fahrzeugskript |
| Eine Runde gewinnen | Beide beherrschen die Bedienung und die Spielregeln |
| Das Spiel auf einem zweiten Rechner starten | Der vollständige Build-Ordner funktioniert außerhalb der Entwicklungsumgebung |

Erklärt euch Änderungen gegenseitig. Notiert kurz Datum, Änderung, verantwortliche Person und das Ergebnis des anschließenden Tests.

## Anforderungen aus den Kursunterlagen

Die folgenden Punkte stammen aus dem bereitgestellten `Organization.md`. Maßgeblich für konkrete Termine und Einreichungswege bleibt euer Kurs.

- Jede Person versteht einen technischen Aspekt besonders gut und zeigt dazu eine eigene technische Demonstration.
- Alle verwendeten Codeabschnitte und Algorithmen müssen verstanden werden.
- Der Pitch umfasst fünf Minuten pro Person, einschließlich Demonstration und verwendeter Algorithmen.
- Mindestens vier Fortschritts- und Feedbacktermine mit einem laufenden Prototyp sind vorgesehen, bevor das Projekt abgeschlossen und präsentiert werden darf.
- Fortschritte werden in einem Projektthread im Moodle-Forum veröffentlicht.
- Für den Präsentationstermin ist ein Showcase-Video mit 30 Sekunden pro Person vorgesehen.
- Abzugeben sind Code und eine ausführbare Spielversion; die Buildgröße liegt unter 1 GB.
- Modelle aus dem Internet oder Store sind zulässig. Für eigens erstellte Modelle nennt das Dokument keinen zusätzlichen Bonus.

Die technischen Dateien ersetzen die Kurstermine, persönlichen Beiträge und Präsentationen nicht. Ob diese organisatorischen Schritte erfüllt sind, ergibt sich aus euren eigenen Einträgen und Teilnahmebelegen.

## Abgabe zusammenstellen

1. Das Unity-Projekt mit `Assets`, `Packages`, `ProjectSettings` und Anleitung vorbereiten.
2. Den vollständigen Ordner `DeliveryCity3D-Spiel` separat bereitstellen.
3. Beide Städte außerhalb des Unity-Editors spielen.
4. Den [Testbericht](04-Testbericht.md) und die [Quellenübersicht](05-Assets-und-Quellen.md) prüfen.
5. Persönliche Beiträge, Kursfortschritte, Pitch und Videos ergänzen.

Der Ordner `Library` wird beim Öffnen von Unity neu erzeugt und gehört nicht in das Projektarchiv. Bei der Windows-Version dagegen müssen Datenordner und Bibliotheken neben der EXE erhalten bleiben.

## Nach einer Änderung neu bauen

1. Beide Szenen speichern und rote Compilerfehler in der **Console** beheben.
2. **File → Build Profiles** öffnen und Windows auswählen.
3. In der **Scene List** zuerst `LittleOakGame`, danach `HarborSide` aktivieren.
4. Den Build in `DeliveryCity3D-Spiel` erstellen; die Startdatei heißt `DeliveryCity3D.exe`.
5. Die erzeugte Anwendung starten und die geänderten Abläufe prüfen.
6. Das tatsächliche Ergebnis im Testbericht festhalten.

