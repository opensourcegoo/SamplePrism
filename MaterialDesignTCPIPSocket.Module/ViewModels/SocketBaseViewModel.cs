using DryIoc.Messages;
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

        private string _infoName = string.Empty;

        public string InfoName
        {
            get => _infoName;
            set => SetProperty(ref _infoName, value);
        }

        #endregion

        /// <summary>
        /// 1,Accept方法是用来接受一个进入的连接请求，并返回一个新的Socket对象来处理该连接。这个方法是同步的，会阻塞当前线程直到有一个连接请求到达。
        /// </summary>
        public SocketBaseViewModel()
        {
            InfoName = "213";
            SocketListen1Command = new DelegateCommand(async () =>
            {
                Socket? listenSocket = null;
                try
                {
                    // 这里是监听按钮的点击事件处理逻辑
                    // 可以在这里添加监听TCP/IP Socket的代码
                    listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPAddress? ipaddr = IPAddress.Any; // 监听所有可用的网络接口
                    IPEndPoint? ipep = new IPEndPoint(ipaddr, 8080); // 监听端口8080
                    listenSocket.Bind(ipep); // 绑定端口
                    listenSocket.Listen(10); // 开始监听，最大连接数为10
                    Console.WriteLine("accept incoming connection");

                    //套接字客户端也是我们要将数据发回去的对象

                    while (true)
                    {
                        Socket? client = await listenSocket.AcceptAsync();
                        #region MyRegion
                        //加for循环来持续接收数据，直到收到特定的退出指令（例如"x"）为止
                        //在这个循环中，我们使用client.Receive方法来接收数据，并将接收到的数据转换为字符串进行处理。每次接收完数据后，我们将其发送回客户端。循环会一直持续，直到收到特定的退出指令（例如"x"）为止。
                        //清理缓冲区和重置接收数量，以准备下一次接收数据
                        //不关心这个任务的结果，所以使用_来丢弃它。这样可以避免编译器警告，同时也表明我们不需要等待这个任务完成。 
                        #endregion
                        _ = Task.Run(async () =>
                          {
                              while (client.Connected)
                              {
                                  byte[] buffer = new byte[1024];
                                  int totalReceiveNum = await client.ReceiveAsync(buffer);
                                  string receiveStr = Encoding.UTF8.GetString(buffer, 0, totalReceiveNum);
                                  InfoName = receiveStr;
                                  //string receiveInfo = Encoding.ASCII.GetString(buffer, 0, totalReceiveNum);
                                  Random random = new Random();
                                  int randomValue = random.Next(40, 101);
                                  byte[] senddata = BitConverter.GetBytes(randomValue);
                                  await client.SendAsync(senddata);
                                  string command = receiveStr.Trim().TrimEnd('\r', '\n').ToUpper();

                                  //string response = command switch
                                  //{
                                  //    "*OPC?" => "1\n",
                                  //    "*STB?" => "0\n",
                                  //    "*IDN?" => "MyInstrument,Model1,SN001,FW1.0\n",
                                  //    "*CLS" => "",
                                  //    "*RST" => "",
                                  //    "*Uni" => InfoName = "UNITY",
                                  //    "X" => "",   // 退出命令
                                  //    _ => $"UNKNOWN:{receiveStr}\n"  // 其他命令，按需修改
                                  //};
                                  //if (!string.IsNullOrEmpty(response))
                                  //{
                                  //    byte[] sendBuffer = Encoding.UTF8.GetBytes(response);
                                  //    await client.SendAsync(sendBuffer.AsMemory(0, sendBuffer.Length));
                                  //}
                                  ////await client.SendAsync(buffer.AsMemory(0, totalReceiveNum)); //推荐使用异步发送数据，以避免阻塞线程
                                  //if (receiveStr == "x")
                                  //    break;
                                  //Array.Clear(buffer, 0, buffer.Length);
                                  totalReceiveNum = 0;
                              }
                          });

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                finally
                {
                    listenSocket?.Close();
                    listenSocket?.Dispose();
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
