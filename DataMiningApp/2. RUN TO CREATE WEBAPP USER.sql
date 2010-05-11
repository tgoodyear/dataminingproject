/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [webapp]    Script Date: 05/11/2010 04:06:36 ******/
CREATE LOGIN [webapp] WITH PASSWORD=N'ÐúÛ+ç²ÞìJ|*Úû x~KSvãXÇì§ÚA^', DEFAULT_DATABASE=[DMP], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [webapp] DISABLE
GO

