# Devarc (v1.0.0)
Devarc is cross-platform development templates.

[WebGL version test build](http://ec2-52-78-42-13.ap-northeast-2.compute.amazonaws.com/a/index.html)

Devarc supports:
- PC, Mobile, WebGL clients.
- NodeJS, CSharp servers.
- MySQL database.

## Install Client ##
* Download & import packages into unity project.
  * https://github.com/devwinsoft/devarc/blob/main/install
  * https://github.com/MessagePack-CSharp/MessagePack-CSharp/releases
  * https://github.com/psygames/UnityWebSocket
    

## Install Server ##
* Download websocket-sharp.dll or source codes for CSharp server.
  * https://github.com/sta/websocket-sharp
    
## Getting Started ##
#### Step 1: Create shared definitions with C#. ####
```csharp
public enum ErrorType
{
    SUCCESS          = 0,
    UNKNOWN          = 1,
    SERVER_ERROR     = 2,
    SESSION_EXPIRED  = 3,
}
```
#### Step 2: Create shared tables with Excel. ####
#### Step 3: Create shared protocols with C#. ####
```csharp
// Protocol from:Client to:AuthServer
namespace C2Auth
{
    public class RequestLogin
    {
        public string accountID;
        public string password;
    }
}

// Protocol from:AuthServer to:Client
namespace Auth2C
{
    public class NotifyLogin
    {
        public ErrorType errorCode;
        public string sessionID;
        public int secret;
    }
}
```
#### Step 4: Create batch files to build. ####
```
IDL.exe -cs-def  {SchemaFolder}\Common.def
IDL.exe -js  {SchemaFolder}\AuthProtocol.idl  {SchemaFolder}\Common.def
TableBuilder.exe -cs {SchemaFolder}\SoundTable.xlsx
move /Y   *.cs    {UnityProjectFolder}\Assets\Scripts\Generated\
```

## License ##

Devarc is provided under [Apache License 2.0].


