mergeInto(LibraryManager.library, {
    LogStyledMessage: function(message, style) {
		LogStyledMessage(UTF8ToString(message), UTF8ToString(style));
    },
	
	LogStyledMessage: function(message) {
		LogStyledMessage(UTF8ToString(message));
    }
});