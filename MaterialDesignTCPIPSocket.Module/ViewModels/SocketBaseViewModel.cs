using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignTCPIPSocket.Module.ViewModels
{
    public class SocketBaseViewModel : BindableBase
    {
        #region Command
        public DelegateCommand SocketListen1Command { get; set; }
        #endregion

        public SocketBaseViewModel()
        {
            SocketListen1Command = new DelegateCommand(async () =>
            {
                // 这里是监听按钮的点击事件处理逻辑
                // 可以在这里添加监听TCP/IP Socket的代码
                Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress ipaddr = IPAddress.Any; // 监听所有可用的网络接口
                IPEndPoint ipep = new IPEndPoint(ipaddr, 8080); // 监听端口8080
                listenSocket.Bind(ipep); // 绑定端口
                listenSocket.Listen(10); // 开始监听，最大连接数为10
                Console.WriteLine("accept incoming connection");

                //套接字客户端也是我们要将数据发回去的对象
                Socket client = null;
                //int totalReceiveNum = 0;
                await Task.Run(() =>
                {
                    client = listenSocket.Accept(); // 同步接受连接（会阻塞线程）
                });

                byte[] buffer = new byte[1024];
                while (true)
                {
                    int totalReceiveNum = client.Receive(buffer);
                    string receiveStr = Encoding.UTF8.GetString(buffer, 0, totalReceiveNum);
                    string receiveInfo = Encoding.ASCII.GetString(buffer, 0, totalReceiveNum);
                    client.Send(buffer);
                    if (receiveStr == "x")
                        break;
                    Array.Clear(buffer, 0, buffer.Length);
                }


                //listenSocket.BeginAccept(new AsyncCallback(AcceptCallback), listenSocket); // 异步接受连接
            });
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            throw new NotImplementedException();
        }
    }
}
