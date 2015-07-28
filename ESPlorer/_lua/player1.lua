local player1 = {}
player1.ID = 1;
player1.IP = "";
player1.AnswerIDs = "";
function player1.SetID(id)
    player1.ID = id;
end
function player1.SetIP(ip)
    player1.IP = ip;
end
function player1.SetAnswer(answerid)
    player1.AnswerIDs = answerid;
end
return player1;
