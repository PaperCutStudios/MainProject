local d = {}
function d.disconnect(socket)
	print("A player has unexpectedly disconnected")
	local p = require("prefs").players
    for key,value in pairs(p) do
        local p = require("player"..value)
        if(p.socket == socket) then
            require("prefs").removeplayer(value)
            break
        end
    end
end
return d