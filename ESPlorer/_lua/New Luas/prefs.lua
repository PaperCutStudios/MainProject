local prefs={}
prefs.diff=2
prefs.time=300
prefs.players={}
prefs.seed=0
prefs.ansnum=0
prefs.started=false
function prefs.getSeed()
	if(prefs.seed == 0) then
		prefs.seed = math.random(99999)
		return prefs.seed
	else
		return prefs.seed
	end
end
function prefs.addanswer()
	prefs.ansnum = prefs.ansnum+1
	if(prefs.ansnum == table.getn(prefs.players)) then
		require("allanswers")()
		prefs.ansnum=0
		prefs.started=false
	end	
end
function prefs.getallstart()
    print("getallstart")
    local startprefs = "hello"..prefs.diff..prefs.time
    print(startprefs)
	return startprefs
end
return prefs
