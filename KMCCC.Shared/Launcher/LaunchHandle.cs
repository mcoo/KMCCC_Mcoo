﻿namespace KMCCC.Launcher
{
	#region

	using System;
    using System.Collections.Generic;
    using System.Diagnostics;
	using Authentication;

	#endregion

	/// <summary>
	///     启动选项
	/// </summary>
	public sealed class LaunchOptions
	{
        private WindowSize size;

        /// <summary>
        ///     最大内存
        /// </summary>
        public int MaxMemory { get; set; }

		/// <summary>
		///     最小内存
		/// </summary>
		public int MinMemory { get; set; }

		/// <summary>
		///     启动的版本
		/// </summary>
		public Version Version { get; set; }

		/// <summary>
		///     使用的验证器
		/// </summary>
		public IAuthenticator Authenticator { get; set; }

		/// <summary>
		///     启动模式
		/// </summary>
		public LaunchMode Mode { get; set; }

		/// <summary>
		///     直接连接的服务器
		/// </summary>
		public ServerInfo Server { get; set; }

		/// <summary>
		///    启动器的Token
		/// </summary>
		public String LauncherToken { get; set; }

		/// <summary>
		///     设置窗口大小
		/// </summary>
		public WindowSize Size { get => size; set { size = value; Features.Add("has_custom_resolution", true); } }

        /// <summary>
        ///     Agent设置（高级内容）
        /// </summary>
        public string AgentPath { get; set; }

        /// <summary>
        ///  设置版本类型（用于1.9以上版本左下角版本号右边的信息显示）
        /// </summary>
        public string VersionType { get; set; }

		/// <summary>
		///  设置版本类型（用于1.9以上版本左下角版本号右边的信息显示）
		/// </summary>
		public string WorkPath { get; set; }

		/// <summary>
		///    启动特性(Feature)设置
		/// </summary>
		public Dictionary<string, object> Features = new Dictionary<string, object>();
    }


    /// <summary>
    ///     启动句柄，基本上也就比较用
    /// </summary>
    public sealed class LaunchHandle
	{
		/// <summary>
		///     只读的验证信息
		/// </summary>
		public readonly AuthenticationInfo Info;

		internal int Code;

		internal LauncherCore Core;

		internal Process Process;

		internal MinecraftLaunchArguments Arguments { get; set; }

		internal LaunchHandle(AuthenticationInfo info)
		{
			Info = info;
		}

		private void Output(object sender, DataReceivedEventArgs e)
		{
			if (e.Data == null)
			{
				Process.OutputDataReceived -= Output;
			}
			else
			{
                Core.Log(this, e.Data);
            }
            e = null;
        }

		private void Error(object sender, DataReceivedEventArgs e)
		{
			if (e.Data == null)
			{
				Process.OutputDataReceived -= Error;
			}
			else
			{
                Core.Log(this, e.Data);
			}
            e = null;
        }

		internal void Work()
		{
            Process.BeginOutputReadLine();
			Process.OutputDataReceived += Output;
			Process.BeginErrorReadLine();
			Process.ErrorDataReceived += Error;
		}
	}

	/// <summary>
	///     启动异常（未启用）
	/// </summary>
	public class LaunchException : Exception
	{
		/// <summary>
		///     启动异常
		/// </summary>
		/// <param name="type">异常类型</param>
		/// <param name="message">异常信息</param>
		public LaunchException(LaunchExceptionType type, string message) : base(message)
		{
			Type = type;
		}

		/// <summary>
		///     启动异常
		/// </summary>
		/// <param name="type">异常类型</param>
		/// <param name="message">异常信息</param>
		/// <param name="innerException">内部异常</param>
		public LaunchException(LaunchExceptionType type, string message, Exception innerException) : base(message, innerException)
		{
			Type = type;
		}

		/// <summary>
		///     异常类型
		/// </summary>
		public LaunchExceptionType Type { get; private set; }
	}

	/// <summary>
	///     异常类型
	/// </summary>
	public enum LaunchExceptionType
	{
		/// <summary>
		///     验证器错误
		/// </summary>
		Authenticator,

		/// <summary>
		///     启动参数操作器错误
		/// </summary>
		ArguementsOperator,

		/// <summary>
		///     启动时错误
		/// </summary>
		LaunchTime,

		/// <summary>
		///     未知
		/// </summary>
		Unknow
	}
}