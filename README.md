
# CS2 RCON Daemon

A simple RCON daemon client for Counter Strike 2 written in Visual Basic. Designed to be used with a PHP web control panel.

Control Panel Repo: (https://github.com/michaelm6769/CS2-RCON-PHP-Console)


## Acknowledgements

 - fpaezf(https://github.com/fpaezf/Valve-RCON-protocol-in-VB.NET)


## API Reference

#### JSON over TCP
```
HOST: 127.0.0.1
PORT: 6060
PROTOCOL: Raw TCP
FORMAT: One JSON message per line (\n-terminated)
```
#### JSON Structure
```
{"cmd": "your command here"}\n
```

#### Daemon Response
```
{
  "ok": true,
  "output_b64": "BASE64_ENCODED_SERVER_OUTPUT"
}
```
## Usage/Examples
Use curl to send status command to daemon using this structure
```bash
echo '{"cmd":"status"}' | curl telnet://127.0.0.1:6060
```
#### Output:
All outputs are base64 encoded
```
Server:  Running ["SERVER_IP:PORT"]
Client:  Disconnected
----- Status -----
@ Current  :  game
source   : console
hostname : "SERVER_NAME"
spawn    : 2
version  : 1.41.2.6/14126 10603 secure  public
steamid  : [G:1:14643603] ()
udp/ip   : "SERVER_IP:PORT"
os/type  : Linux dedicated
players  : 0 humans, 2 bots (0 max) (not hibernating) (unreserved)
---------spawngroups----
loaded spawngroup(  1)  : SV:  [1: de_lake | main lump | mapload]
loaded spawngroup(  2)  : SV:  [2: prefabs/misc/team_select | main lump | mapload | point_prefab]
loaded spawngroup(  3)  : SV:  [3: prefabs/misc/counterterrorist_team_intro | main lump | mapload | point_prefab]
loaded spawngroup(  4)  : SV:  [4: prefabs/misc/terrorist_team_intro | main lump | mapload | point_prefab]
loaded spawngroup(  5)  : SV:  [5: prefabs/misc/counterterrorist_team_intro_variant2 | main lump | mapload | point_prefab]
loaded spawngroup(  6)  : SV:  [6: prefabs/misc/terrorist_team_intro_variant2 | main lump | mapload | point_prefab]
loaded spawngroup(  7)  : SV:  [7: prefabs/misc/terrorist_wingman_intro | main lump | mapload | point_prefab]
loaded spawngroup(  8)  : SV:  [8: prefabs/misc/counterterrorist_wingman_intro | main lump | mapload | point_prefab]
loaded spawngroup(  9)  : SV:  [9: prefabs/misc/end_of_match | main lump | mapload | point_prefab]
loaded spawngroup( 10)  : SV:  [10: maps/de_lake_skybox | main lump | mapload | point_prefab]
---------players--------
  id     time ping loss      state   rate adr name
   0      BOT    0    0     active      0 'Romanov'
   1      BOT    0    0     active      0 'Crasswater'
#end
