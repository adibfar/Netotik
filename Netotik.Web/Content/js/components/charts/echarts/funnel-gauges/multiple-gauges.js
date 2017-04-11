$(window).on("load",function(){require.config({paths:{echarts:"robust-assets/js/plugins/charts/echarts"}}),require(["echarts","echarts/chart/funnel","echarts/chart/gauge"],function(a){var b=a.init(document.getElementById("multiple-gauge"));multigaugeOptions={tooltip:{formatter:"{a} <br/>{b} : {c}%"},toolbox:{show:!0,feature:{mark:{show:!0},restore:{show:!0},saveAsImage:{show:!0}}},calculable:!0,series:[{name:"Speed",type:"gauge",z:3,min:0,max:220,splitNumber:11,axisLine:{lineStyle:{width:10}},axisTick:{length:15,lineStyle:{color:"auto"}},splitLine:{length:20,lineStyle:{color:"auto"}},title:{textStyle:{fontWeight:"bolder",fontSize:20,fontStyle:"italic"}},detail:{textStyle:{fontWeight:"bolder"}},data:[{value:40,name:"km/h"}]},{name:"Rotating speed",type:"gauge",center:["15%","55%"],radius:"50%",min:0,max:7,endAngle:45,splitNumber:7,axisLine:{lineStyle:{width:8}},axisTick:{length:12,lineStyle:{color:"auto"}},splitLine:{length:20,lineStyle:{color:"auto"}},pointer:{width:5},title:{offsetCenter:[0,"-30%"]},detail:{textStyle:{fontWeight:"bolder"}},data:[{value:1.5,name:"x1000 r/min"}]},{name:"Fuel meter",type:"gauge",center:["85%","50%"],radius:"50%",min:0,max:2,startAngle:135,endAngle:45,splitNumber:2,axisLine:{lineStyle:{color:[[.2,"#ff4500"],[.8,"#48b"],[1,"#228b22"]],width:8}},axisTick:{splitNumber:5,length:10,lineStyle:{color:"auto"}},axisLabel:{formatter:function(a){switch(a+""){case"0":return"E";case"1":return"Gas";case"2":return"F"}}},splitLine:{length:15,lineStyle:{color:"auto"}},pointer:{width:2},title:{show:!1},detail:{show:!1},data:[{value:.5,name:"gas"}]},{name:"Meter",type:"gauge",center:["85%","50%"],radius:"50%",min:0,max:2,startAngle:315,endAngle:225,splitNumber:2,axisLine:{lineStyle:{color:[[.2,"#ff4500"],[.8,"#48b"],[1,"#228b22"]],width:8}},axisTick:{show:!1},axisLabel:{formatter:function(a){switch(a+""){case"0":return"H";case"1":return"Water";case"2":return"C"}}},splitLine:{length:15,lineStyle:{color:"auto"}},pointer:{width:2},title:{show:!1},detail:{show:!1},data:[{value:.5,name:"gas"}]}]},b.setOption(multigaugeOptions),$(function(){function a(){setTimeout(function(){b.resize()},200)}$(window).on("resize",a),$(".menu-toggle").on("click",a),clearInterval(c);var c=setInterval(function(){multigaugeOptions.series[0].data[0].value=(100*Math.random()).toFixed(2)-0,multigaugeOptions.series[1].data[0].value=(7*Math.random()).toFixed(2)-0,multigaugeOptions.series[2].data[0].value=(2*Math.random()).toFixed(2)-0,multigaugeOptions.series[3].data[0].value=(2*Math.random()).toFixed(2)-0,b.setOption(multigaugeOptions,!0)},2e3)})})});