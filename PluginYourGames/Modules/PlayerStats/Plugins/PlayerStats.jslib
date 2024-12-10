mergeInto(LibraryManager.library,
{
	InitStats_js: function () {
        var returnStr = statsSaves;
		var bufferSize = lengthBytesUTF8(returnStr) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(returnStr, buffer, bufferSize);
		return buffer;
    },
	
	GetStats_js: function () {
        GetStats();
    },
	
	SetState_js: function (key, value) {
        SetState(UTF8ToString(key), value);
    },
	
	SetAllStats_js: function (jsonStats) {
        SetAllStats(UTF8ToString(jsonStats));
    }
});