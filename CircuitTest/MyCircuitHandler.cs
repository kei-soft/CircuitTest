using Microsoft.AspNetCore.Components.Server.Circuits;

namespace CircuitTest
{
    /// <summary>
    /// Blazor 서버 앱의 Circuit 상태를 추적하는 핸들러입니다.
    /// 클라이언트 연결 상태에 따라 IsConnected 값을 변경합니다.
    /// </summary>
    public class MyCircuitHandler : CircuitHandler
    {
        /// <summary>
        /// 현재 클라이언트가 연결되어 있는지 여부를 나타냅니다.
        /// </summary>
        public bool IsConnected { get; private set; } = true;

        /// <summary>
        /// Circuit이 열릴 때 호출됩니다. 연결 상태를 true로 설정합니다.
        /// </summary>
        /// <param name="circuit">현재 Circuit 인스턴스</param>
        /// <param name="cancellationToken">취소 토큰</param>
        public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            IsConnected = true;
            return base.OnCircuitOpenedAsync(circuit, cancellationToken);
        }

        /// <summary>
        /// Circuit이 닫힐 때 호출됩니다. 연결 상태를 false로 설정합니다.
        /// </summary>
        /// <param name="circuit">현재 Circuit 인스턴스</param>
        /// <param name="cancellationToken">취소 토큰</param>
        public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            IsConnected = false;
            return base.OnCircuitClosedAsync(circuit, cancellationToken);
        }

        /// <summary>
        /// 클라이언트와의 연결이 끊겼을 때 호출됩니다. 연결 상태를 false로 설정합니다.
        /// </summary>
        /// <param name="circuit">현재 Circuit 인스턴스</param>
        /// <param name="cancellationToken">취소 토큰</param>
        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            IsConnected = false;
            return base.OnConnectionDownAsync(circuit, cancellationToken);
        }
    }
}
