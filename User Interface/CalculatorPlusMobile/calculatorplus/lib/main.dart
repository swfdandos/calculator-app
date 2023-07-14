import 'package:flutter/material.dart';

void main() {
  runApp(const MyApp());
}
const operatorcolor = Color(0xff272727);
const buttonColor = Color(0xff191919);
const orangecolor = Color(0xffD9802E);
class MyApp extends StatelessWidget {
  const MyApp({super.key});


  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Calculator+',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // Try running your application with "flutter run". You'll see the
        // application has a blue toolbar. Then, without quitting the app, try
        // changing the primarySwatch below to Colors.green and then invoke
        // "hot reload" (press "r" in the console where you ran "flutter run",
        // or simply save your changes to "hot reload" in a Flutter IDE).
        // Notice that the counter didn't reset back to zero; the application
        // is not restarted.
        primarySwatch: Colors.blue,
      ),
      home: const CalcScreen(),
    );
  }
}

class CalcScreen extends StatefulWidget {
  const CalcScreen({Key? key}) : super(key: key);

  @override
  State<CalcScreen> createState() => _CalcScreenState();
}

class _CalcScreenState extends State<CalcScreen> {
  //Variables
  double firstnumber = 0.0;
  double secondnumber = 0.0;
  var input = '12X';
  var output = '';
  var operation = '';
  var hideInput = false;
  var outputSize = 34.0;




  onButtonClick(value) {
    //if value is AC
    if (value == 'AC') {
      input = '';
      output = '';
    } else if( value =='H'){

    } else if (value == '<') {
      if (input.isNotEmpty) {
        input = input.substring(0, input.length - 1);
      }
    }
    else if (value == '=') {
      if (input.isNotEmpty) {
        var userInput = input;
        userInput = input.replaceAll('X', '*');


        //Parser p = Parser();
        //Expression expression = p.parse(userInput);
       // ContextModel cm = ContextModel();
       // var finalValue = expression.evaluate(EvaluationType.REAL, cm);
        //output = finalValue.toString();



        if (output.endsWith(".0")) {
          output = output.substring(0, output.length - 2);
        }
        input = output;
        hideInput = true;
        outputSize = 52.0;
      }
    } else {
      input = input + value;
      hideInput = false;
      outputSize = 34.0;
    }

    setState(() {});
  }



  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text(
            "Calculator+",
          style: TextStyle(
            color: orangecolor,
          ),
        ),
        backgroundColor: operatorcolor,
        actions: [
          Container(margin: const EdgeInsets.only(right: 50), child: IconButton(onPressed: (){}, icon: const Icon(Icons.more_vert,color: orangecolor,))),
        ],
      ),
      backgroundColor: Colors.black,
      body: Column(
        children: [
          //Input and output area
          Expanded(
              child: Container(
                  width: double.infinity,
                  padding: const EdgeInsets.all(12),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.end,
                    crossAxisAlignment: CrossAxisAlignment.end,
                    children: [
                      Text(
                        hideInput ? '' : input,
                        style: const TextStyle(
                          fontSize: 48,
                          color: Colors.white,
                        ),
                      ),
                      const SizedBox(
                        height: 20,
                      ),
                      Text(
                        output,
                        style: TextStyle(
                          fontSize: outputSize,
                          color: Colors.white.withOpacity(0.7),
                        ),
                      ),
                      const SizedBox(
                        height: 30,
                      )
                    ],
                  )
              )
          ),
          Row(children: [
            button(
                text: 'AC', buttonBGcolor: operatorcolor, tColor: orangecolor),
            button(
                text: '<', buttonBGcolor: operatorcolor, tColor: orangecolor),
            button( text: 'H', buttonBGcolor: operatorcolor, tColor: orangecolor),
            button(
                text: '/', buttonBGcolor: operatorcolor, tColor: orangecolor),
          ]),
          Row(children: [
            button(text: '7'),
            button(text: '8'),
            button(text: '9'),
            button(
                text: 'X', tColor: orangecolor, buttonBGcolor: operatorcolor),
          ]),
          Row(children: [
            button(text: '4'),
            button(text: '5'),
            button(text: '6'),
            button(
                text: '-', tColor: orangecolor, buttonBGcolor: operatorcolor),
          ]),
          Row(children: [
            button(text: '1'),
            button(text: '2'),
            button(text: '3'),
            button(
                text: '+', tColor: orangecolor, buttonBGcolor: operatorcolor),
          ]),
          Row(children: [
            button(
                text: '%', tColor: orangecolor, buttonBGcolor: operatorcolor),
            button(text: '0'),
            button(text: '.'),
            button(text: '=', buttonBGcolor: orangecolor),
          ]),
        ],
      ),
    );
  }

  Widget button({text, tColor = Colors.white, buttonBGcolor = buttonColor}) {
    return Expanded(
        child: Container(
          margin: const EdgeInsets.all(8),
          child: ElevatedButton(
              style: ElevatedButton.styleFrom(
                shape:
                RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
                padding: const EdgeInsets.all(22),
                backgroundColor: buttonBGcolor,
              ),
              onPressed: () => onButtonClick(text),
              child: Text(
                text,
                style: TextStyle(
                  fontSize: 18,
                  color: tColor,
                  fontWeight: FontWeight.bold,
                ),
              )
          ),
        )
    );
  }
}

