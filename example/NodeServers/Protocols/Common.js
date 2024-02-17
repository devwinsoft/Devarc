const ErrorType = {
	SUCCESS              : 0,
	UNKNOWN              : 1,
	DATABASE_ERROR       : 2,
	PROTOCOL_ERROR       : 3,
	SERVER_ERROR         : 4,
	SESSION_EXPIRED      : 5,
	SESSION_REMAIN       : 6,
	INVALID_PASSWORD     : 7,
	INVALID_SECRET       : 8,
};
Object.freeze(ErrorType);

class CommonResult
{
	/**
	 * @param {ErrorType} errorCode - ErrorType
	 */
	constructor() {
		this.errorCode = 0;
	}
	Init(packet) {
		this.errorCode = packet[0];
	}
	ToArray() {
		const data =
		[
			this.errorCode,
		];
		return data;
	}
}

class CustomSigninResult
{
	/**
	 * @param {ErrorType} errorCode - ErrorType
	 * @param {string} secret - string
	 */
	constructor() {
		this.errorCode = 0;
		this.secret = "";
	}
	Init(packet) {
		this.errorCode = packet[0];
		this.secret = packet[1];
	}
	ToArray() {
		const data =
		[
			this.errorCode,
			this.secret,
		];
		return data;
	}
}

class GoogleCodeResult
{
	/**
	 * @param {ErrorType} errorCode - ErrorType
	 * @param {string} code - string
	 */
	constructor() {
		this.errorCode = 0;
		this.code = "";
	}
	Init(packet) {
		this.errorCode = packet[0];
		this.code = packet[1];
	}
	ToArray() {
		const data =
		[
			this.errorCode,
			this.code,
		];
		return data;
	}
}

class GoogleSigninResult
{
	/**
	 * @param {ErrorType} errorCode - ErrorType
	 * @param {string} account_id - string
	 * @param {string} access_token - string
	 * @param {string} refresh_token - string
	 * @param {int} expires_in - int
	 * @param {string} secret - string
	 */
	constructor() {
		this.errorCode = 0;
		this.account_id = "";
		this.access_token = "";
		this.refresh_token = "";
		this.expires_in = 0;
		this.secret = "";
	}
	Init(packet) {
		this.errorCode = packet[0];
		this.account_id = packet[1];
		this.access_token = packet[2];
		this.refresh_token = packet[3];
		this.expires_in = packet[4];
		this.secret = packet[5];
	}
	ToArray() {
		const data =
		[
			this.errorCode,
			this.account_id,
			this.access_token,
			this.refresh_token,
			this.expires_in,
			this.secret,
		];
		return data;
	}
}

class GoogleRefreshResult
{
	/**
	 * @param {ErrorType} errorCode - ErrorType
	 * @param {string} access_token - string
	 * @param {string} refresh_token - string
	 * @param {int} expires_in - int
	 */
	constructor() {
		this.errorCode = 0;
		this.access_token = "";
		this.refresh_token = "";
		this.expires_in = 0;
	}
	Init(packet) {
		this.errorCode = packet[0];
		this.access_token = packet[1];
		this.refresh_token = packet[2];
		this.expires_in = packet[3];
	}
	ToArray() {
		const data =
		[
			this.errorCode,
			this.access_token,
			this.refresh_token,
			this.expires_in,
		];
		return data;
	}
}

const GenderType = {
	None                 : 0,
	Male                 : 1,
	Female               : 2,
};
Object.freeze(GenderType);

class Account
{
	/**
	 * @param {string} nickName - string
	 * @param {int} level - int
	 */
	constructor() {
		this.nickName = "";
		this.level = 0;
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
, CommonResult
, CustomSigninResult
, GoogleCodeResult
, GoogleSigninResult
, GoogleRefreshResult
, GenderType
, Account
}

