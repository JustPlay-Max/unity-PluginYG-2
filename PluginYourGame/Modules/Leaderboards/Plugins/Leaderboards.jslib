mergeInto(LibraryManager.library,
{
	SetLeaderboard_js: function (nameLB, score)
	{
		SetLeaderboard(UTF8ToString(nameLB), score);
	},
	
	GetLeaderboard_js: function (nameLB, quantityTop, quantityAround, photoSizeLB, auth)
	{
		GetLeaderboard(UTF8ToString(nameLB), quantityTop, quantityAround, UTF8ToString(photoSizeLB), auth);
	}
});