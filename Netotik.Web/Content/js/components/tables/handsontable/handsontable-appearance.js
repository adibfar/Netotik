document.addEventListener("DOMContentLoaded",function(){function a(a,b,c,d,e,f,g){Handsontable.renderers.TextRenderer.apply(this,arguments),b.style.fontWeight="bold",b.style.color="green",b.style.background="#CEC"}function b(a,b,c,d,e,f,g){Handsontable.renderers.TextRenderer.apply(this,arguments),parseInt(f,10)<0&&(b.className="make-me-red"),f&&""!==f?("Nissan"===f&&(b.style.fontStyle="italic"),b.style.background=""):b.style.background="#EEE"}var c,d,e=[["","Kia","Nissan","Toyota","Honda"],["2014",-5,"",12,13],["2015","",-11,14,13],["2016","",15,-12,"readOnly"]];Handsontable.renderers.registerRenderer("negativeValueRenderer",b),c=document.getElementById("formatting"),d=new Handsontable(c,{data:e,afterSelection:function(a,b,c,d){var e=this.getCellMeta(c,d);e.readOnly?this.updateSettings({fillHandle:!1}):this.updateSettings({fillHandle:!0})},cells:function(b,c,d){var e={};return 0!==b&&"readOnly"!==this.instance.getData()[b][c]||(e.readOnly=!0),0===b?e.renderer=a:e.renderer="negativeValueRenderer",e}});var f,c=document.getElementById("borders");f=Handsontable(c,{data:Handsontable.helper.createSpreadsheetData(70,20),rowHeaders:!0,fixedColumnsLeft:2,fixedRowsTop:2,colHeaders:!0,customBorders:[{range:{from:{row:1,col:1},to:{row:3,col:4}},top:{width:2,color:"#5292F7"},left:{width:2,color:"orange"},bottom:{width:2,color:"red"},right:{width:2,color:"magenta"}},{row:2,col:2,left:{width:2,color:"red"},right:{width:1,color:"green"}}]});var f,e=[["","Kia","Nissan","Toyota","Honda"],["2013",10,11,12,13],["2014",20,11,14,13],["2015",30,15,12,13]],c=document.getElementById("highlighting");f=Handsontable(c,{data:e,minRows:5,minCols:6,currentRowClassName:"currentRow",currentColClassName:"currentCol",rowHeaders:!0,colHeaders:!0}),f.selectCell(2,2);var g,h=document.getElementById("mobilesTablets");g=new Handsontable(h,{data:Handsontable.helper.createSpreadsheetData(100,100),rowHeaders:!0,colHeaders:!0,fixedRowsTop:2,fixedColumnsLeft:2})});