var CommentBox = React.createClass({
	getInitialState: function(){
		var self = this;	// Need to declare here, not in e.g., componentDidMount
		return {data: this.props.initialData};
	},
	loadCommentsFromServer: function(){
		var url = this.props.url + self.answerID;
		var result = eusAPI.get(url);
		this.setState({ data: result});
	},
	handleCommentSubmit: function(comment){
		// submit to the server and refresh the list
		// Create json array with UserID, AnswerID, and Rating

		var comments = this.state.data;
		var newComments = comments.concat([comment]);
		this.setState({data: newComments});

		var obj = {};
		obj["UserID"] = self.userID;
		obj["AnswerID"] = self.answerID;   // $(this).data('id');

		obj["Comment"] = comment.Text;

		// Create json string
		var data = JSON.stringify(obj);

		// Create json string with Filter to send to API
		var jsonString = JSON.stringify({ "Filter": "CommentPost", "FilterData": data });

		var result = eusAPI.post(this.props.submitUrl, jsonString);
	},
	componentDidMount: function(){
		// Get hidden field values.		
		self.userID = document.getElementById('UserID').value;
		self.userName = $('#UserName').val();
		self.answerID = $('#AnswerID').val();
		self.answerTitle = $('#AnswerTitle').val();

		// Create URL so user can return to topic page
		self.topicTitleID = $("#TopicID").val();
		self.topicTitle = $('#TopicTitle').val();
		self.topicTitleURL = $('#TopicTitleURL').val();
		self.returnURL = $('#BaseUrl').val() + "eusvote/" + self.topicTitleID + "/" + self.topicTitleURL;
		$("#topicReturnURL").html("<a href=" + self.returnURL + "> Back to topic: " + self.topicTitle + "</a>");

		$('#answerTitle').text(self.answerTitle);   // Set comment title
		//this.loadCommentsFromServer();
		//window.setInterval(this.loadCommentsFromServer, this.props.pollInterval);
	},
	render: function(){
		return (
			<div className="commentBox">
				<Header />
				<CommentList data={this.state.data} />
				<CommentForm onCommentSubmit={this.handleCommentSubmit} />
			</div>
		);
	}
});

// Header that can be customized for each comment page
var Header = React.createClass({
	render: function(){
		return (
			<div>
				<br/>
				<div id="topicReturnURL">
				</div>

				<div>
					<h2><span id='answerTitle'></span></h2>
					[Rate Button]  eusScore: <span id="span-eusScore"></span>
				</div><br/>
			</div>
		);
	}
});

// Comment container to hold list of comments
var CommentList = React.createClass({
	render: function(){
		var commentNodes = this.props.data.map(function(comment){
			return(
				<Comment author={comment.Author} text={comment.Text}>
				</Comment>
			);
		});
		return (
			<div className="commentList">
				{commentNodes}
			</div>
		);
	}
});

var Comment = React.createClass({
	render: function(){
		var auth = this.props.author.toString();

		//var converter = new Showdown.converter();
		//var rawMarkup = converter.makeHtml(this.props.children.toString());
		return (
			<div className="comment">
				<div>
					{this.props.author}
				</div>
				<div>
					{this.props.text}
				</div><br/>
			</div>
		);
	}
});

var CommentForm = React.createClass({
	handleSubmit: function(e) {
		// TODO: If not logged in, the Submit button should be disabled. But do a check anyways to make sure that user is logged in.

		e.preventDefault();

		var author = self.userName;
		var text = this.refs.text.getDOMNode().value.trim();

		if (!text){
			return;
		}
		// todo: send request to the server
		this.props.onCommentSubmit({Author: author, Text: text});

		// reset the fields to blanks	
		this.refs.text.getDOMNode().value = '';
		return;
	},
	render: function(){
		return (
			<form className="commentForm" onSubmit={this.handleSubmit}>
				<input type="text" placeholder="Type comment" ref="text"/>
				<input type="submit" value="Submit" />
			</form>
		);
	}
});