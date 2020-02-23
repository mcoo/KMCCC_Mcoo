namespace KMCCC.Launcher
{
	#region

	using System;
	using System.Runtime.InteropServices;

	#endregion

	public static class LaunchHandleExtensions
	{
		public static bool SetTitle(this LaunchHandle handle, string title)
		{
			try
			{
				if (SetWindowText(handle.Process.MainWindowHandle, title) != 0)
				{
					return true;
				}
				else
				{
					return false;
				}
				
			}
			catch
			{
				return false;
			}
		}

		public static string GetTile(this LaunchHandle handle)
		{
			try
			{
				return handle.Process.MainWindowTitle;
			}
			catch
			{
				return null;
			}
		}
		public static bool Setloc(this LaunchHandle handle, int x,int y ,int width,int height)
		{
			try
			{
				if (MoveWindow(handle.Process.MainWindowHandle,x,y,width,height,true) != 0)
				{
					return true;
				}
				else
				{
					return false;
				}

			}
			catch
			{
				return false;
			}
		}

		public static void Kill(this LaunchHandle handle)
		{
			handle.Process.Kill();
		}


		[DllImport("User32.dll")]
		public static extern int SetWindowText(IntPtr winHandle, string title);
		/// <summary>
		/// 设置目标窗体大小，位置
		/// </summary>
		/// <param name="hWnd">目标句柄</param>
		/// <param name="x">目标窗体新位置X轴坐标</param>
		/// <param name="y">目标窗体新位置Y轴坐标</param>
		/// <param name="nWidth">目标窗体新宽度</param>
		/// <param name="nHeight">目标窗体新高度</param>
		/// <param name="BRePaint">是否刷新窗体</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		public static extern int MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool BRePaint);
	}
}