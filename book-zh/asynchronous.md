# 异步处理
线程 被定义为程序的执行路径。每个线程都定义了一个独特的控制流。如果您的应用程序涉及到复杂的和耗时的操作，那么设置不同的线程执行路径往往是有益的，每个线程执行特定的工作。  

由于计算机处理器是有计算瓶颈的，所以无法让所有的事情都按照单线顺序的方式逐个处理，为了提升处理容量，我们经常会需要使用异步并行的方式来解决计算问题。  

.Net 平台拥有自己的线程库 `System\Threading` ，更多关于线程的使用可以查询相关接口。  

这里我们谈一谈如何更简单地处理线程问题，也就是异步处理。  

在其它语言里可以认为是异步编程终级解决方案的 `async/await`。  

在这里我们使用的任务数据类型是 `tsk`。

## 异步声明
那么如何声明一个函数是异步的呢？使用 `~>` 就可以了。

没错，真的是使用 `~>` 就可以了。

例如：
```
async() ~> (out: i32) { 
    <-(12)
}
```
一旦一个方法被声明为异步方法，编译器会自动给返回值套上 `tsk<>` 类型包裹，这个方法就可以被异步执行了。

正常直接调用只会得到一个 `tsk` 数据。

例如：
```
result := async()  # result 是一个 Task 数据
```
接下来我们再看看如何让它异步等待执行。
## 异步等待
与声明一样，我们只需要使用 `<~ function()` 就可以声明执行一个异步方法。

例如：
```
result := <~ async()
...
```
声明异步等待后，程序执行到这里就会暂时停止后面的功能，直到 `async` 函数执行完毕后，将 `out` 的值赋值给 `result`，再继续执行。
## 异步使用条件
异步等待只能在异步声明的函数里使用。

例如：
```
# 正确
async() ~> (out: i32) {
    <~ tsks.delay(5000)    # 等待一段时间
    <- (12)
}
# 错误
async() -> (out: i32) {
    <~ tsks.delay(5000)    # 不能被声明
    <- (12)
}
```
## 空返回值
如果异步方法没有返回值，它也会同样返回一个 `tsk` 数据，外部调用一样可以等待。

我们可以选择等待不获取数据，也可以选择不等待获取数据。

例如：
```
async() ~> () {
    <~ tsks.delay(5000)    # 等待一段时间
}

<~ async()     # 正确

task := async()    # 正确，获取了 Task
```
## Lambda
对于lambda，我们也可以使用异步，同样使用 `~>` 即可。

例如：
```
_ = arr.filter( {it ~> it > 5} )
```
### [下一章](generic.md)