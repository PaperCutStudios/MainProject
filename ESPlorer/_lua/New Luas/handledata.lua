local handledata = ...
return function(socket, data)
	print("Receiving data: "..data)
	if(data:sub(1,1) == "0") then
		print("Getting playernum")
		local pnum = require("getavailplayer").getavailplayer()
		print(pnum)
		if(pnum~=0) then
			require("player"..pnum).connect(socket)
		end
		pnum=nil
	elseif(tonumber(data:sub(1,1))<=4) then
	
	end
end
