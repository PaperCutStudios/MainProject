local player = {}
player.ID = 0;
player.IP = "";
player.AnswerIDs = "";
function player.SetID(id)
    player.ID = id;
end
function player.SetIP(ip)
    player.IP = ip;
end
function player.SetAnswer(answerid)
    player.AnswerIDs = answerid;
end
return player;