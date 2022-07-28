using System.Collections.Generic;
using RSG;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace UIWidgetsSample {
    
    public class MaterialAppBarSample : UIWidgetsSamplePanel {

        protected override Widget createWidget() {
            return new MaterialApp(
                showPerformanceOverlay: false,
                home: new MaterialAppBarWidget());
        }

        protected override void OnEnable() {
            FontManager.instance.addFont(Resources.Load<Font>(path: "fonts/MaterialIcons-Regular"), "Material Icons");
            base.OnEnable();
        }
    }
    
    public class MaterialAppBarWidget : StatefulWidget {
        public MaterialAppBarWidget(Key key = null) : base(key) {
        }

        public override State createState() {
            return new MaterialAppBarWidgetState();
        }
    }

    public class MaterialAppBarWidgetState : State<MaterialAppBarWidget> {
        Choice _selectedChoice = Choice.choices[0];

        GlobalKey<ScaffoldState> _scaffoldKey = GlobalKey<ScaffoldState>.key();

        VoidCallback _showBottomSheetCallback;

        public override void initState() {
            base.initState();
            this._showBottomSheetCallback = this._showBottomSheet;
        }

        void _showBottomSheet() {
            this.setState(() => { this._showBottomSheetCallback = null; });

            this._scaffoldKey.currentState.showBottomSheet((BuildContext subContext) => {
                ThemeData themeData = Theme.of(subContext);
                return new Container(
                    decoration: new BoxDecoration(
                        border: new Border(
                            top: new BorderSide(
                                color: themeData.disabledColor))),
                    child: new Padding(
                        padding: EdgeInsets.all(32.0f),
                        child: new Text("This is a Material persistent bottom sheet. Drag downwards to dismiss it.",
                            textAlign: TextAlign.center,
                            style: new TextStyle(
                                color: themeData.accentColor,
                                fontSize: 16.0f))
                    )
                );
            }).closed.Then((object obj) => {
                if (this.mounted) {
                    this.setState(() => { this._showBottomSheetCallback = this._showBottomSheet; });
                }

                return new Promise();
            });
        }

        void _select(Choice choice) {
            this.setState(() => { this._selectedChoice = choice; });
        }

        public override Widget build(BuildContext context) {
            return new Scaffold(
                key: this._scaffoldKey,
                appBar: new AppBar(
                    title: new Text("Basic AppBar"),
                    actions: new List<Widget> {
                        new IconButton(
                            icon: new Icon(Choice.choices[0].icon),
                            //color: Colors.blue,
                            onPressed: () => { this._select((Choice.choices[0])); }
                        ),
                        new IconButton(
                            icon: new Icon(Choice.choices[1].icon),
                            //color: Colors.blue,
                            onPressed: () => { this._select((Choice.choices[1])); }
                        ),

                        new PopupMenuButton<Choice>(
                            onSelected: this._select,
                            itemBuilder: (BuildContext subContext) => {
                                List<PopupMenuEntry<Choice>> popupItems = new List<PopupMenuEntry<Choice>>();
                                for (int i = 2; i < Choice.choices.Count; i++) {
                                    popupItems.Add(new PopupMenuItem<Choice>(
                                        value: Choice.choices[i],
                                        child: new Text(Choice.choices[i].title)));
                                }

                                return popupItems;
                            }
                        )
                    }
                ),
                body: new Padding(
                    padding: EdgeInsets.all(16.0f),
                    child: new ChoiceCard(choice: this._selectedChoice)
                ),
                floatingActionButton: new FloatingActionButton(
                    backgroundColor: Colors.redAccent,
                    child: new Icon(Unity.UIWidgets.material.Icons.add_alert),
                    onPressed: this._showBottomSheetCallback
                ),
                drawer: new Drawer(
                    child: new ListView(
                        padding: EdgeInsets.zero,
                        children: new List<Widget> {
                            new ListTile(
                                leading: new Icon(Unity.UIWidgets.material.Icons.account_circle),
                                title: new Text("Login"),
                                onTap: () => { }
                            ),
                            new Divider(
                                height: 2.0f),
                            new ListTile(
                                leading: new Icon(Unity.UIWidgets.material.Icons.account_balance_wallet),
                                title: new Text("Wallet"),
                                onTap: () => { }
                            ),
                            new Divider(
                                height: 2.0f),
                            new ListTile(
                                leading: new Icon(Unity.UIWidgets.material.Icons.accessibility),
                                title: new Text("Balance"),
                                onTap: () => { }
                            )
                        }
                    )
                )
            );
        }
    }
    
    class Choice {
        public Choice(string title, IconData icon) {
            this.title = title;
            this.icon = icon;
        }

        public readonly string title;
        public readonly IconData icon;

        public static List<Choice> choices = new List<Choice> {
            new Choice("Car", Unity.UIWidgets.material.Icons.directions_car),
            new Choice("Bicycle", Unity.UIWidgets.material.Icons.directions_bike),
            new Choice("Boat", Unity.UIWidgets.material.Icons.directions_boat),
            new Choice("Bus", Unity.UIWidgets.material.Icons.directions_bus),
            new Choice("Train", Unity.UIWidgets.material.Icons.directions_railway),
            new Choice("Walk", Unity.UIWidgets.material.Icons.directions_walk)
        };
    }

    class ChoiceCard : StatelessWidget {
        public ChoiceCard(Key key = null, Choice choice = null) : base(key: key) {
            this.choice = choice;
        }

        public readonly Choice choice;

        public override Widget build(BuildContext context) {
            TextStyle textStyle = Theme.of(context).textTheme.display1;
            return new Card(
                color: Colors.white,
                child: new Center(
                    child: new Column(
                        mainAxisSize: MainAxisSize.min,
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: new List<Widget> {
                            new Icon(this.choice.icon, size: 128.0f, color: textStyle.color),
                            new RaisedButton(
                                child: new Text(this.choice.title, style: textStyle),
                                onPressed: () => {
                                    SnackBar snackBar = new SnackBar(
                                        content: new Text(this.choice.title + " is chosen !"),
                                        action: new SnackBarAction(
                                            label: "Ok",
                                            onPressed: () => { }));

                                    Scaffold.of(context).showSnackBar(snackBar);
                                })
                        }
                    )
                )
            );
        }
    }
}