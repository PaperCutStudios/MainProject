local handledata = {}
function handledata.handle(socket, data)
	print("Receiving data: "..data)
    --apparently, without this, the socket wont store and data cannot be sent back.
    socket:send("0")
	if(data:sub(1,1) == "0") then
		print("Getting playernum")
		local pnum = require("getavailplayer").getavailplayer()
		if(pnum~=0) then
			require("player"..pnum).connect(socket)
		end
		pnum=nil
	elseif(tonumber(data:sub(1,1))<=4) then
	    if(data:sub(2,2) == "0") then
            require("player"..data:sub(1,1)).disconnect()
        end
	end
end
return handledata
