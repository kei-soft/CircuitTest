# Blazor Server - JSDisconnectedException 방지 예제

Blazor Server 프로젝트에서 JavaScript 모듈을 import한 후, 컴포넌트가 dispose 될 때 `JSDisconnectedException` 오류가 발생하는 문제를 해결하는 방법을 다룹니다. 이 오류는 클라이언트와 서버 간 연결이 끊긴 후 JavaScript interop을 호출하려고 할 때 발생합니다.

본 예제는 `CircuitHandler`를 활용하여 클라이언트 연결 상태를 추적하고, 연결된 상태일 때만 JS 모듈을 안전하게 dispose 하도록 구성되어 있습니다.

🔗 관련 블로그 포스트: [https://keistory.tistory.com/1752](https://keistory.tistory.com/1752)

---

## 문제 설명

Blazor Server 앱에서 JavaScript 모듈을 import하여 사용 중, 페이지를 새로 고침하거나 클라이언트와 서버 간의 연결이 끊어질 경우, JSInterop을 통해 모듈을 dispose 하려는 코드에서 `JSDisconnectedException` 예외가 발생할 수 있습니다.

이 예외는 서버가 이미 클라이언트와의 회로(circuit)를 종료했기 때문에 JavaScript 호출이 불가능한 상태에서 발생합니다.

---

## 해결 전략

이 문제를 해결하기 위해 `CircuitHandler`를 구현하여 다음을 수행합니다.

- 클라이언트와의 회로가 열릴 때 연결 상태를 true로 설정
- 회로가 닫히거나 연결이 끊겼을 때 연결 상태를 false로 설정
- 컴포넌트가 dispose 될 때 연결 상태를 확인한 후, 연결된 경우에만 JS 모듈을 dispose

이 방식은 불필요한 try-catch 처리 없이 깔끔하게 예외를 회피할 수 있습니다.
