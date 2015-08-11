function HandleSocketData (Socket,DataStream)
    if(DataStream:byte(1) == string.byte("0")) then
        local serverfuncs = require("ServerFuncs1")
        serverfuncs.InitialJoin(Socket)
    elseif(DataStream:byte(2) == string.byte("0")) then
        local serverfuncs = require("ServerFuncs2")
        
    end
end
function OnConnection(Socket)
    Socket:on("receive", HandleSocketData)

end
--ServerListening
Server:listen(80,OnConnection)
