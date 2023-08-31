const ErrorType = {
	SUCCESS              : 0,
	UNKNOWN              : 1,
	SERVER_ERROR         : 2,
	SESSION_EXPIRED      : 3,
	INVALID_PASSWORD     : 4,
	INVALID_SECRET       : 5,
};
Object.freeze(ErrorType);

class Character
{
	/**
	 * @param {string} nickName - string
	 * @param {int} level - int
	 */
	constructor(nickName, level) {
		this.nickName = nickName;
		this.level = level;
	}
	Init(packet) {
		this.nickName = packet[0];
		this.level = packet[1];
	}
	ToArray() {
		const data =
		[
			this.nickName,
			this.level,
		];
		return data;
	}
}

module.exports =
{ ErrorType
, Character
}
